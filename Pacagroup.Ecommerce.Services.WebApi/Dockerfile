# Etapa de build con SDK 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copiamos toda la solución
COPY . .

# Nos ubicamos en el proyecto WebApi
WORKDIR /src/Pacagroup.Ecommerce.Services.WebApi

# Restauramos dependencias
RUN dotnet restore

# Publicamos en modo Release
RUN dotnet publish -c Release -o /app/out

# Etapa de runtime con ASP.NET 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiamos los artefactos publicados desde la etapa de build
COPY --from=build-env /app/out ./

# Importante: usar el appsettings.Docker.json
ENV ASPNETCORE_ENVIRONMENT=Docker

# (Opcional) expone el puerto interno si quieres documentarlo
# EXPOSE 8080

ENTRYPOINT ["dotnet", "Pacagroup.Ecommerce.Services.WebApi.dll"]



# Con esta linea ejecuto
# docker image build -t pacagroup.ecommerce:1.0.1 -f .\Pacagroup.Ecommerce.Services.WebApi\Dockerfile .
# Con esta linea reviso
# docker image ls
# Remover la imagen intermedia
# docker image rm -f id_imagen_intermedia
# Para conocer que contenedores estan corriendo.
# docker container ls
# Para crear contenedor
# docker container run  --name pacagroup.ecommerce -d -p 8050:8080 pacagroup.ecommerce:1.0.1
# o docker run -p 8050:8080 pacagroup.ecommerce:1.0.1
# para revisar http://localhost:8050/swagger/index.html
