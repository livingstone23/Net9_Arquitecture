using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface;
using System.Reflection;



namespace Pacagroup.Ecommerce.Application.Main;



/// <summary>
/// Clase que permite configurar los servicios de la capa de aplicación.
/// </summary>
public static class ConfigureServices
{

    /// <summary>
    /// Instala los servicios de la capa de aplicación en el contenedor de dependencias.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddScoped<ICustomersApplication, CustomersApplication>();
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(
            cfg => { /* aquí puedes añadir profiles manualmente si quieres */ },
            Assembly.GetExecutingAssembly()
        );

        return services;
    
    }

}

