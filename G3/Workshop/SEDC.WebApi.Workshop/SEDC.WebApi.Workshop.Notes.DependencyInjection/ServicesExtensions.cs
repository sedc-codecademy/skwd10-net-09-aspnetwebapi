using Microsoft.Extensions.DependencyInjection;
using SEDC.WebApi.Workshop.Notes.Sevices;
using SEDC.WebApi.Workshop.Notes.Sevices.Interfaces;

namespace SEDC.WebApi.Workshop.Notes.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static IServiceCollection RegisterServicesDependencies
            (this IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
