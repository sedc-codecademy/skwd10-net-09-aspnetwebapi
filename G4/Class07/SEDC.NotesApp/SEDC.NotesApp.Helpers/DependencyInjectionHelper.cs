using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.Implementations;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Implementations;
using SEDC.NotesApp.Services.Interfaces;

namespace SEDC.NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<NotesAppDbContext>(x =>
            x.UseSqlServer("Server=.;Database=NotesAppDb;Trusted_Connection=True"));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
        }
    }
}
