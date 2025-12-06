namespace Pacagroup.Ecommerce.Services.WebApi.Modules;



public static class RedisExtensions
{

    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
            //options.InstanceName = "PacagroupEcommerce_";
        });
        return services;
    }


}