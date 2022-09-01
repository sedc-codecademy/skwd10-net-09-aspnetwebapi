using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notes.Contracts.Services;
using Notes.Domain.Repositories;
using Notes.Domain.UnitOfWork;
using Notes.Services.Services;
using Notes.Shared;
using Notes.Storage.Database;
using Notes.Storage.Repositories;
using Notes.Storage.UnitOfWork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string databaseConnectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");
builder.Services
    .AddDbContext<INotesDbContext, NotesDbContext>(options =>
    {
        options.UseSqlServer(databaseConnectionString);
    })
    .AddScoped<INoteRepository, NoteRepository>()
    .AddScoped<ITagRepository, TagRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<INoteService, NoteService>();

var appConfig = builder.Configuration.GetSection("Auth");

//var secret = appConfig.GetValue<string>("SecretKey");

builder.Services.Configure<Auth>(appConfig);

var auth = appConfig.Get<Auth>();
var secret = Encoding.ASCII.GetBytes(auth.SecretKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secret)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
