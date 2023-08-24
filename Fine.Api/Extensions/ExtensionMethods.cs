using Fine.Api.DataAcess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Fine.Api.Extensions
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddFineDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<FineDbContext>(config =>
            config.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}
