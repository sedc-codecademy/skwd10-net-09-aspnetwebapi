using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SEDC.WebApi.Workshop.Notes.Common.Middleware;
using SEDC.WebApi.Workshop.Notes.Common.Models;
using SEDC.WebApi.Workshop.Notes.DependencyInjection;
using Serilog;
using System.Text;

namespace SEDC.WebApi.Workshop.Notes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
                });
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            //Logs Everything
            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.File("SeriLogs.txt"));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                      {
                        {
                          new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                        });
            });

            // Configuring AppSettings section
            var appConfig = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appConfig);

            // Using AppSettings
            var appSettings = appConfig.Get<AppSettings>();
            var secret = Encoding.ASCII.GetBytes(appSettings.Secret);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }
            );

            builder.Services
                .RegisterDataDependencies(appSettings.ConnectionString)
                .RegisterServicesDependencies();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseCors("MyPolicy");

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.Run();
        }
    }
}