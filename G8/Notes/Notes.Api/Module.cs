using AutoMapper;
using HashidsNet;
using Notes.Application.Models;
using Notes.Domain.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Module
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IHashids>((sp) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var secret = configuration["Secret"];
                return new Hashids(secret);
            });

            services.AddSingleton<MapperConfiguration>(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateUserModel, User>()
                  .ForMember(m => m.Username, m => m.MapFrom(x => x.UserName));
            }));

            services.AddScoped<IMapper>(sp =>
            {
                MapperConfiguration config = sp.GetRequiredService<MapperConfiguration>();
                return config.CreateMapper();
            });
            return services;
        }
    }
}
