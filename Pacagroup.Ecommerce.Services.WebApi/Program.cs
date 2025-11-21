using Microsoft.AspNetCore.Cors.Infrastructure;
using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Infrastructure.Repository;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Authentication;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string MyCorsPolicy = "MyCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyCorsPolicy, corsBuilder =>
    {
        corsBuilder
            .WithOrigins(builder.Configuration["Config:OriginCors"]!) // ej: "https://midominio.com"
            .AllowAnyHeader()
            .AllowAnyMethod();
        // .AllowCredentials(); // solo si lo necesitas
    });
});



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



//Metodos para implementar inyeccion de dependencias de los proyectos
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

//Agregamos el metodo extension para seguridad
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddSwagger();



var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Generar documentación JSON de Swagger
    app.UseSwagger();

    // Habilitar interfaz interactiva de usuario de Swagger
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.RoutePrefix = "swagger";
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.ShowExtensions();
    });
    
}

app.UseHttpsRedirection();

app.UseCors(MyCorsPolicy); // ⬅️ Politica de cors


// Agregamos el middleware de autenticación
app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
