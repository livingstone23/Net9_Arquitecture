using Pacagroup.Ecommerce.Application.Validator;



namespace Pacagroup.Ecommerce.Services.WebApi.Modules;



public static class ValidatorExtensions
{
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddTransient<SignUpDtoValidator>();


        return services;
    }
}