using AutoMapper;
using MovieManager.Application.Mapper;
using MovieManager.Application.Repositories;
using MovieManager.Application.Services;
using MovieManager.Application.Services.Implementation;
using MovieManager.Domain.Models;
using MovieManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Movie>, MovieRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddSingleton(sp => ModelMapper.GetConfiguration());
builder.Services.AddScoped(sp =>
{
    MapperConfiguration config = sp.GetRequiredService<MapperConfiguration>();
    return config.CreateMapper();
});

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
