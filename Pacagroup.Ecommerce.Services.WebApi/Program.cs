using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Infrastructure.Repository;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Authentication;
using Pacagroup.Ecommerce.Services.WebApi.Modules.HealthCheck;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger;
using Pacagroup.Ecommerce.Services.WebApi.Validator;
using Pacagroup.Ecommerce.Services.WebApi.Versioning;
using Pacagroup.Ecommerce.Tranversal.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string MyCorsPolicy = "MyCorsPolicy";

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyCorsPolicy, corsBuilder =>
    {
        corsBuilder
            .WithOrigins(builder.Configuration["Config:OriginCors"]!) // ej: "http://localhost:60468/"
            .AllowAnyHeader()
            .AllowAnyMethod();
        // .AllowCredentials(); // solo si lo necesitas
    });
});

builder.Services.AddControllers();

// OpenAPI (.NET 9)
builder.Services.AddOpenApi();

// Servicios de dominio / infra / aplicación
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

// Logging transversal (Serilog + MSSQL + File + Console)
builder.Services.AddTransversalCollection(builder.Configuration);

// Host usa Serilog
builder.Host.UseSerilog();


// Seguridad / JWT
builder.Services.AddAuth(builder.Configuration);


// Versionamiento de API
builder.Services.AddVersioning();


// Swagger (config de módulos)
builder.Services.AddSwagger();


// ✅ Health Checks (tu extensión)
builder.Services.AddHealthCheck(builder.Configuration);


// Habilito el llamado al fluent validation
builder.Services.AddValidator();

var app = builder.Build();

// 👇 Esto es clave para que Swagger sepa las versiones
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Variable de ayuda para entorno
var isDevOrDocker =
    app.Environment.IsDevelopment() ||
    string.Equals(app.Environment.EnvironmentName, "Docker", StringComparison.OrdinalIgnoreCase);

//foreach (var desc in apiVersionDescriptionProvider.ApiVersionDescriptions)
//{
//    Log.Information("API VERSION FOUND => ApiVersion: {ApiVersion}, GroupName: {GroupName}, Deprecated: {Deprecated}",
//        desc.ApiVersion, desc.GroupName, desc.IsDeprecated);
//}


// Swagger en Development **y** Docker
if (isDevOrDocker)
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        // 🔥 Recorremos todas las versiones descubiertas
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant()
            );
        }

        options.RoutePrefix = "swagger"; // UI => /swagger
        options.DisplayRequestDuration();
        options.EnableDeepLinking();
        options.ShowExtensions();
    });
}

//if (isDevOrDocker)
//{
//    app.UseSwagger();

//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//        c.RoutePrefix = "swagger"; // UI => /swagger
//        c.DisplayRequestDuration();
//        c.EnableDeepLinking();
//        c.ShowExtensions();
//    });
//}

// Middleware de logging de Serilog
app.UseSerilogRequestLogging();

// HTTPS solo fuera de Docker (para evitar el warning de puerto https)
if (!string.Equals(app.Environment.EnvironmentName, "Docker", StringComparison.OrdinalIgnoreCase))
{
    app.UseHttpsRedirection();
}

app.UseCors(MyCorsPolicy);

// Autenticación / Autorización
app.UseAuthentication();



app.UseAuthorization();

app.MapControllers();

// Health check simple (para probes de Kubernetes, Azure, etc.)
// Endpoint de health para la API (solo checks con tag "database")
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = reg => reg.Tags.Contains("database"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// UI de HealthChecks
app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";      // http://localhost:5039/health-ui
    options.ApiPath = "/health-ui-api"; // API que usa la UI
});



// Bloque de control de errores + arranque
try
{
    Log.Information("Starting Application API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
