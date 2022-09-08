using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NotesApp.DataAccess;

namespace SEDC.NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<NotesAppDbContext>(x =>
            x.UseSqlServer("Server=.;Database=NotesAppDb;Trusted_Connection=True"));
        }
    }
}
