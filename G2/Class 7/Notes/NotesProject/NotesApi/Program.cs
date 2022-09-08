using Microsoft.EntityFrameworkCore;
using Notes.Domain.Repositories;
using Notes.Domain.UnitOfWork;
using Notes.Storage.Database;
using Notes.Storage.Repositories;
using Notes.Storage.UnitOfWork;

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
    .AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
