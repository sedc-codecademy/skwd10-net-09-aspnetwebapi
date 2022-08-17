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
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterModule(this IServiceCollection services) 
        {
            //service registration
            services.AddTransient<IUserService, UserService>();

            //repository registration
            services.AddTransient<IRepository<UserDto>, UserEntityRepository>();

            return services;
        }
    }
}
