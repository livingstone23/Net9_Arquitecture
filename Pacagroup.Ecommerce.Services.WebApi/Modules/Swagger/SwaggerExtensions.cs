using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;



namespace Pacagroup.Ecommerce.Services.WebApi.Modules;



/// <summary>
/// Clase estática para extensiones de Swagger
/// </summary>
public static class SwaggerExtensions
{

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {


        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(c =>
        {

            //Seccion no es necesaria porque se ha migrado a la clase ConfigureSwaggerOptions

            #region MyRegion_Comentada

            //c.SwaggerDoc("v1", new OpenApiInfo
            //{
            //    Version = "v1",
            //    Title = "Test API Market",
            //    Description = "A simple example ASP.NET Core Web API. ",
            //    TermsOfService = new Uri("https://pacagroup.com/terms"),
            //    Contact = new OpenApiContact
            //    {
            //        Name = "Livingstone Cano",
            //        Email = "livingstone23@gmail.com",
            //        Url = new Uri("https://pacagroup.com/contact")
            //    },
            //    License = new OpenApiLicense
            //    {
            //        Name = "Use under LICX",
            //        Url = new Uri("https://pacagroup.com/licence")
            //    }
            //});

            #endregion

            /// Incluir comentarios XML
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            
            /// Obtener la ruta completa del archivo XML
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            c.IncludeXmlComments(xmlPath);


            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new List<string>() { } }
            });

            /// Habilitar anotaciones
            c.EnableAnnotations();
        });

        return services;

    }

}