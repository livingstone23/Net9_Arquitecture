using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;



namespace Pacagroup.Ecommerce.Services.WebApi.Versioning;



/// <summary>
/// Clase de extensiones para la configuración de versionamiento de API.
/// </summary>
public static class VersioningExtensions
{
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;

            // ✅ Versión en la URL: /api/v1/...
            //Para versionar utilizando la URL Punto01
            //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            //Para versionar utilizando encabezado Punto02
            options.ApiVersionReader = new HeaderApiVersionReader("x-version");
            //Parar versionar utilizando el segmento en la url Punto03
            //options.ApiVersionReader = new UrlSegmentApiVersionReader();

        });

        services.AddVersionedApiExplorer(options =>
        {

            options.GroupNameFormat = "'v'VVV";          // v1, v2, v3
            //Solo utilizar la versión en la URL con el punto03
            //options.SubstituteApiVersionInUrl = true;    // sustituye {version:apiVersion} en la ruta

        });

        return services;
    }
}