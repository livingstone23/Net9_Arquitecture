namespace Pacagroup.Ecommerce.Services.WebApi.Validator;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddTransient<>();
    }
}