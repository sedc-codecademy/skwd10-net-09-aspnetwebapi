using AutoMapper;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Repositories;
using Notes.Application.Services;
using Notes.Application.Services.Implementation;
using Notes.Domain.Models;
using Notes.Infrastracture;
using Notes.Infrastracture.EmailSender;
using Notes.Infrastracture.EntityFramework;
using Notes.Infrastracture.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IRepository<Note>, NoteRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddSingleton<MapperConfiguration>(new MapperConfiguration(cfg =>
{
}));
builder.Services.AddScoped<IMapper>(sp =>
{
    MapperConfiguration config = sp.GetRequiredService<MapperConfiguration>();
    return config.CreateMapper();
});
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IHashids, Hashids>();
builder.Services.AddScoped<IHashids>((sp) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var secret = configuration["Secret"];
    return new Hashids(secret);
});
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
