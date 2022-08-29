//Microsoft.EntityFrameworkCore.Design
//Microsoft.AspNetCore.Authentication.JwtBearer
//AutoMapper.Extensions.Microsoft.DependencyInjection
//Serilog.AspNetCore

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Configurations;
using NotesApp.Utilities;
using Serilog;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.File("Logs/logs.txt"));

builder.Services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.MaxDepth = 64;
    options.JsonSerializerOptions.IncludeFields = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// automapper setup
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure AppSettings class
var appConfig = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appConfig);

// using AppSettings class
var appSettings = appConfig.Get<AppSettings>();
var secret = Encoding.ASCII.GetBytes(appSettings.Secret);

// authentication middleware configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secret),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.RegisterModule(appSettings.ConnectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// If you set these in the reverse order, your requests will get 401 responses all the time
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
