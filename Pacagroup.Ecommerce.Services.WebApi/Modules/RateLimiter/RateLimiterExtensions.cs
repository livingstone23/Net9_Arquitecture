using Microsoft.AspNetCore.RateLimiting;



namespace Pacagroup.Ecommerce.Services.WebApi.Modules;



/// <summary>
/// Clase de extensiones para la configuración de limitación de tasa (Rate Limiting).
/// </summary>
public static class RateLimiterExtensions
{


    /// <summary>
    /// Extensión para agregar la configuración de limitación de tasa al contenedor de servicios.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddRatelimiting(this IServiceCollection services, IConfiguration configuration)
    {

        var fixedWindowPolicy = "fixedWindow";
        
        services.AddRateLimiter(configureOptions => {
            configureOptions.AddFixedWindowLimiter(policyName: fixedWindowPolicy, fixedWindow =>
            {
                fixedWindow.PermitLimit = int.Parse(configuration["RateLimiting:PermitLimit"]);
                fixedWindow.Window = TimeSpan.FromSeconds(int.Parse(configuration["RateLimiting:Window"]));
                fixedWindow.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                fixedWindow.QueueLimit = int.Parse(configuration["RateLimiting:QueueLimit"]);
            });

            // Política por defecto, permite usar la política fixedWindow
            // Donde se responde con el código 429 cuando se exceden los límites
            configureOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests; 
        });

        return services;

    }
}