using FineApi.Dal.Repository;
using FineApi.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FineApi.Dal
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FineDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitOfWorkRepository), typeof(UnitOfWorkRepository));

            AddCorsService(services); // Call the AddCorsService method here

            return services;
        }

        private static void AddCorsService(IServiceCollection services)
        {
            /*services.AddCors(opt =>
            {
                opt.AddPolicy("DevCors", corsBuilder =>
                {
                    corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
                opt.AddPolicy("ProdCors", corsBuilder =>
                {
                    corsBuilder.WithOrigins("https://mygpsadmin.mygps.ge:4435")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });*/
        }
    }
}