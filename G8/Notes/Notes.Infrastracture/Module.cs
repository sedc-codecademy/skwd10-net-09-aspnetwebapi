using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes.Application.Models;
using Notes.Application.Repositories;
using Notes.Application.Services;
using Notes.Domain.Models;
using Notes.Infrastracture;
using Notes.Infrastracture.EmailSender;
using Notes.Infrastracture.EntityFramework;
using Notes.Infrastracture.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddInfrastracture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<Note>, BaseRepository<Note>>();
            services.AddScoped<IRepository<User>, BaseRepository<User>>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            return services;
        }
    }
}
