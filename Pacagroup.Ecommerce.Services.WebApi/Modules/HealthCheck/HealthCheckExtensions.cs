


namespace Pacagroup.Ecommerce.Services.WebApi.Modules.HealthCheck;



/// <summary>
/// Clase que contiene extensiones para la configuración de Health Checks.
/// </summary>
public static class HealthCheckExtensions
{

    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        // HealthCheck de SQL Server
        services.AddHealthChecks()
            .AddSqlServer(
                configuration.GetConnectionString("NorthwindConnection"),
                name: "SQL Server",
                tags: new[] { "database" }
            );

        // UI que consulta el endpoint cada 5 segundos
        services.AddHealthChecksUI(setup =>
            {
                setup.SetEvaluationTimeInSeconds(5);              // 🔁 cada 5 segundos
                setup.AddHealthCheckEndpoint(
                    "API Ecommerce - DB",
                    "/health"                                     // endpoint que vamos a mapear abajo
                );
            })
            .AddInMemoryStorage();

        return services;
    }

}