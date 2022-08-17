using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.DAL;
using NotesApp.DAL.Repositories;
using NotesApp.DataModels;
using NotesApp.Services.Implementations;
using NotesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    //Microsoft.Extensions.DependencyInjection.Abstractions
    //Microsoft.EntityFrameworkCore.SqlServer
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterModule(this IServiceCollection services) 
        {
            services.AddDbContext<NotesAppDbContext>(options => 
            {
                options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=NotesDb;Trusted_Connection=True");
            });

            //service registration
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INoteService, NoteService>();

            //repository registration
            //services.AddTransient<IRepository<UserDto>, UserStaticDbRepository>();
            //services.AddTransient<IRepository<NoteDto>, NoteStaticDbRepoistory>();
            services.AddTransient<IRepository<UserDto>, UserEntityRepository>();
            services.AddTransient<IRepository<NoteDto>, NoteEntityRepository>();

            return services;
        }
    }
}
