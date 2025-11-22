using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Infrastructure.Repository;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Authentication;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger;
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

// Swagger (config de módulos)
builder.Services.AddSwagger();

var app = builder.Build();

// Variable de ayuda para entorno
var isDevOrDocker =
    app.Environment.IsDevelopment() ||
    string.Equals(app.Environment.EnvironmentName, "Docker", StringComparison.OrdinalIgnoreCase);

// Swagger en Development **y** Docker
if (isDevOrDocker)
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.RoutePrefix = "swagger"; // UI => /swagger
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.ShowExtensions();
    });
}

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
