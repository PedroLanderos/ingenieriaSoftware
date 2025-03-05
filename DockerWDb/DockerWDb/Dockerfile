# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto .csproj y restaurar las dependencias
COPY DockerWDb/DockerWDb.csproj DockerWDb/
WORKDIR /src/DockerWDb
RUN dotnet restore

# Copiar el resto del código fuente
COPY . .

# Publicar la aplicación
RUN dotnet publish -c Release -o /app/publish

# Etapa final: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar los archivos publicados desde la etapa anterior
COPY --from=build /app/publish .

# Establecer la variable de entorno para usar el puerto 5000
ENV ASPNETCORE_URLS=http://+:5000

# Configurar el punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "DockerWDb.dll"]
