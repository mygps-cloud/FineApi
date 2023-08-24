using FineApi.Dal.Repository;
using FineApi.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FineApi.Dal;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<FineDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))); 
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IUnitOfWorkRepository), typeof(UnitOfWorkRepository));
        return services;
    }
}