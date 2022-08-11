using Notes.Application.Repositories;
using Notes.Application.Services;
using Notes.Application.Services.Implementation;
using Notes.Domain.Models;
using Notes.Infrastracture.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IRepository<Note>, NoteRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
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