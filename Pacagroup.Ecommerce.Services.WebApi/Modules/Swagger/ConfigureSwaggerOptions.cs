using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;



namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger;



/// <summary>
/// Clase para configurar las opciones de Swagger.
/// Definimos y agregamos los documentos para cada versión de la API descubierta.
/// </summary>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{

    /// <summary>
    ///  Permite la exploración de las versiones de la API.
    /// </summary>
    readonly IApiVersionDescriptionProvider provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
    /// </summary>
    /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        // add a swagger document for each discovered API version
        // note: you might choose to skip or document deprecated API versions differently
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Version = description.ApiVersion.ToString(),
            Title = "Pacagroup Technology Services API Market",
            Description = "A simple example ASP.NET Core Web API",
            TermsOfService = new Uri("https://pacagroup.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "livingstone Cano",
                Email = "livingstone23@gmail.com",
                Url = new Uri("https://pacagroup.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Use under LICX",
                Url = new Uri("https://pacagroup.com/licence")
            }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}