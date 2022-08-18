using ASPNETAPI_G2_L6.Database;
using Microsoft.EntityFrameworkCore;

namespace ASPNETAPI_G2_L6.Resgistrations
{
    public static class DatabaseRegistration
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection serviceCollection,
                                                          string connectionString)
        {
            serviceCollection.AddDbContext<NotesDbContext>(actions =>
            {
                actions.UseSqlServer(connectionString);
            });

            return serviceCollection;
        }
    }
}
