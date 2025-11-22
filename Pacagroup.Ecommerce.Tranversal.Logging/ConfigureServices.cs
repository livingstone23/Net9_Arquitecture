using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;



namespace Pacagroup.Ecommerce.Tranversal.Logging;



/// <summary>
/// Clase para la configuración de servicios de logging
/// </summary>
public static class ConfigureServices
{
    
    public static IServiceCollection AddTransversalCollection(this IServiceCollection services, IConfiguration configuration)
    {

        // Configuración básica de Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()

            .Enrich.WithProperty("Application","Pacagegroup.Ecommerce")
            .WriteTo.Console(outputTemplate:"[{Timestamp:HH:mm:ss} {level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            .WriteTo.File(
                path:"logs/log-.txt",
                rollingInterval:RollingInterval.Day,
                retainedFileCountLimit:30,
                outputTemplate: "[{Timestamp:HH:mm:ss} {level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"
                )
            .WriteTo.MSSqlServer(
                connectionString: configuration.GetConnectionString("NorthwindConnection"),
                sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                {
                    TableName = "Logs",
                    AutoCreateSqlTable = true
                },
                restrictedToMinimumLevel: LogEventLevel.Warning)
            .CreateLogger();

        // Integrar Serilog con el ILogger de .NET
        services.AddLogging();

        //Registrar la inyección de dependencia para IAppLogger<T>
        services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));

        return services;

    }



}