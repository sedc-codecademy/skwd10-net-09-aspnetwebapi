using Microsoft.AspNetCore.Authentication.Cookies;
using Notes.Api.Worker;
using Notes.Application;
using Notes.Application.Exceptions;
using Notes.Application.Services;
using Notes.Infrastracture.Services;
using Serilog;
using System.Diagnostics;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var corsPolicy = "CorsPolicy";
// Add services to the container.
// Sto e Dependency injection
// lifetime 

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddInfrastracture(builder.Configuration);
builder.Services.AddConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };

        opts.Events.OnRedirectToAccessDenied = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
        opts.Cookie.SameSite = SameSiteMode.None;
        opts.Cookie.HttpOnly = true;
        opts.Cookie.IsEssential = true;
    });
builder.Services.AddAuthorization(opts =>
{
    // requirment -> handlers
    opts.AddPolicy(SystemPolicies.MustHaveId, builder => builder.RequireClaim(ClaimTypes.NameIdentifier));
});
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();
Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(builder.Configuration)
                .CreateLogger();
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);
builder.Services.AddHttpClient<IUserExternalService, UserExternalService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExternalServices:DataProviderApi"]);
});
builder.Services.AddCors(setup =>
{
    setup.AddPolicy(corsPolicy, policyBuilder => policyBuilder
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});
// asdadasdasd
// next()
//
//builder.Services.AddHostedService<ImportUsersBackgroundWorker>();
var app = builder.Build();
// Configure the HTTP request pipeline.

// sto e middelware
// request pipeline
//redosledot
//
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(corsPolicy);// sho e cross origin request
app.UseGlobalExceptionHandler(); // + 
app.Use(async (context, next) =>
{
    try
    {
        var stopwatch = new Stopwatch();
        Log.Logger.Information("Request - ");

        stopwatch.Start();
        await next();
        stopwatch.Stop();
        if (stopwatch.ElapsedMilliseconds > 500)
        {
            Log.Logger.Warning("Request took {duration}", stopwatch.ElapsedMilliseconds);
        }
        Log.Logger.Information("Response");
    }
    catch (NotFoundException ex)
    {
        Log.Logger.Warning("An exception occured", ex);
        throw;
    }
    catch (ExecutionNotAllowedException ex)
    {
        Log.Logger.Warning("An exception occured", ex);
        throw;
    }
    catch (ValidationException ex)
    {
        Log.Logger.Warning("An exception occured", ex);
        throw;
    }
    catch (Exception ex)
    {
        Log.Logger.Error("An exception occured", ex);
        throw;
    }
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
