using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.AdoImplementations;
using SEDC.NotesApp.DataAccess.DapperImplementations;
using SEDC.NotesApp.DataAccess.Implementations;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Implementations;
using SEDC.NotesApp.Services.Interfaces;

namespace SEDC.NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<NotesAppDbContext>(x =>
            //x.UseSqlServer("Server=10.10.10.10;Database=NotesAppDb;Trusted_Connection=True"));
            //x.UseSqlServer("Server=.\\SQLEXPRESS;Database=NotesAppDb;Trusted_Connection=True"));

            services.AddDbContext<NotesAppDbContext>(x =>
            x.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IUserService, UserService>();
        }

        public static void InjectAdoRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(x => new NoteAdoRepository(connectionString));
        }

        public static void InjectDapperRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(x => new NoteDapperRepository(connectionString));
        }
    }
}
