﻿using FineApi.Service.Abstractions;
using FineApi.Service.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FineApi.Service;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IUserCarInformationService,UserCarInformationService>();
        services.AddScoped<IReceivedSmsService, ReceivedSmsService>();
        services.AddScoped<ILoggerService, LoggerService>();
        return services;
    }
}