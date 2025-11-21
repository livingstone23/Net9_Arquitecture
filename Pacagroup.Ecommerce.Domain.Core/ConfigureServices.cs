using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Domain.Interface;



namespace Pacagroup.Ecommerce.Domain.Core;



/// <summary>
/// Clase para configurar los servicios del dominio
/// </summary>
public static class ConfigureServices
{

    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {

        services.AddScoped<ICustomersDomain, CustomersDomain>();
        services.AddScoped<IUsersDomain, UsersDomain>();

        return services;

    }

}