using Notes.Application.Services;
using Notes.Application.Services.Implementation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();
            return services;
        }
    }
}
