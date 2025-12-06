using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;



namespace Pacagroup.Ecommerce.Infrastructure.Repository;



/// <summary>
/// Clase estática para configurar los servicios de infraestructura
/// Metodo de extensión para IServiceCollection
/// </summary>
public static class ConfigureServices
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {

        services.AddSingleton<DapperContext>();
        services.AddScoped<ICustomersRepository, CustomersRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();


        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;

    }

}