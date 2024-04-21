using FineApi.Service.Abstractions;
using FineApi.Service.SendNotification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FineApi.Service;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IUserCarInformationService,UserCarInformationService>();
        services.AddScoped<IReceivedSmsService, ReceivedSmsService>();
        services.AddScoped<INotificationServiceManager, NotificationServiceManager>();
        services.AddScoped<ILoggerService, LoggerService>();
        services.AddScoped<ISmsSenderService, SmsSenderService>();
        return services;
    }
}