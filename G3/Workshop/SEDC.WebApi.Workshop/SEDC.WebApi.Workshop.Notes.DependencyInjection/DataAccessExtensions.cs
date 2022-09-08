using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.WebApi.Workshop.Notes.DataAccess;
using SEDC.WebApi.Workshop.Notes.DataAccess.Repositories;
using SEDC.WebApi.Workshop.Notes.DataModels.Models;

namespace SEDC.WebApi.Workshop.Notes.DependencyInjection
{
    public static class DataAccessExtensions
    {
        public static IServiceCollection RegisterDataDependencies
            (this IServiceCollection services, string connectionString = null)
        {
            if(!string.IsNullOrWhiteSpace(connectionString))
            {
                services.AddDbContext<NotesDbContext>(x =>
                            x.UseSqlServer(connectionString));
            }

            //services.AddTransient<IRepository<Note>, NoteRepository>();
            //services.AddTransient<IRepository<Note>, NoteEFRepository>();
            //services.AddTransient<IRepository<Note>, NoteAdoRepository>();
            services.AddTransient<IRepository<Note>, NoteDapperRepository>();
            //services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<User>, UserEFRepository>();
            

            return services;
        }
    }
}
