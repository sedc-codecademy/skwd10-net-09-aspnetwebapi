using SEDC.WebApi.Workshop.Notes.Common.Models;
using SEDC.WebApi.Workshop.Notes.DependencyInjection;

namespace SEDC.WebApi.Workshop.Notes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuring AppSettings section
            var appConfig = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appConfig);

            // Using AppSettings
            var appSettings = appConfig.Get<AppSettings>();

            builder.Services
                .RegisterDataDependencies(appSettings.ConnectionString)
                .RegisterServicesDependencies();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}