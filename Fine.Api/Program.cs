using FineApi.Dal;
using FineApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// ...

static IServiceCollection AddCorsService(IServiceCollection services)
{
    services.AddCors((opt) =>
    {
        opt.AddPolicy("Cors", (corsBuilder) =>
        {
            corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
    });
    return services;
}


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Call the AddCorsService method to configure CORS policies
AddCorsService(builder.Services);

builder.Services.AddEndpointsApiExplorer();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}
var app = builder.Build();

// ...
app.UseCors("Cors");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
try
{
    app.Run();
}
catch (Exception ex)
{
    throw;
}