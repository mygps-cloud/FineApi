using Fine.Api.Application.ServiceContracts;
using Fine.Api.Application.Services;
using Fine.Api.DataAccess.Contracts.Repositories;
using Fine.Api.DataAcess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Fine.Api.CrossCutting
{
    public static class IoCRegister
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            AddServices(services);
            AddRepositories(services);
            AddCorsService(services);
            return services;
        }

        private static IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserCarInformationService, UserCarInformationService>();
            services.AddScoped<IReceivedSmsService, ReceivedSmsService>();
            return services;
        }
        private static IServiceCollection AddRepositories(IServiceCollection services) 
        {
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IReceivedSmsRepository, ReceivedSmsRepository>();
            services.AddScoped<IUsercarInformationRepository, UserCarInformationRepository>();
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
            return services;
        }
        private static IServiceCollection AddCorsService(IServiceCollection services)
        {
            services.AddCors((opt) =>
            {
                opt.AddPolicy("DevCors", (corsBuilder) =>
                {
                    corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
            return services;
        }
    }
}
