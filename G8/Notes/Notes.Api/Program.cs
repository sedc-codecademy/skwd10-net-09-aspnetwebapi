using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Notes.Api.Middlewares;
using Notes.Application;
using Notes.Application.Exceptions;
using Notes.Infrastracture.EntityFramework;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddInfrastracture(builder.Configuration);
builder.Services.AddConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(opts =>
//    {
//        opts.Events.OnRedirectToLogin = (context) =>
//        {
//            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//            return Task.CompletedTask;
//        };

//        opts.Events.OnRedirectToAccessDenied = (context) =>
//        {
//            context.Response.StatusCode = StatusCodes.Status403Forbidden;
//            return Task.CompletedTask;
//        };
//    });
builder.Services.AddAuthorization(opts =>
{
    // requirment -> handlers
    opts.AddPolicy(SystemPolicies.MustHaveId, builder => builder.RequireClaim(ClaimTypes.NameIdentifier));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
