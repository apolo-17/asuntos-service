# Imagen base para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Imagen para la compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar solo el archivo de proyecto para restaurar primero
COPY ["./AsuntosService.csproj", "AsuntoService/"]
RUN dotnet restore "AsuntoService/AsuntosService.csproj"

# Copiar todo el código fuente
COPY . .
WORKDIR "/src/AsuntoService"

# Asegurar que la restauración de paquetes se ha ejecutado antes de la compilación
RUN test -f obj/project.assets.json || dotnet restore

# Compilar el proyecto
RUN dotnet build -c Release -o /app/build

# Publicar el proyecto
FROM build AS publish
WORKDIR "/src/AsuntoService"
RUN dotnet publish -c Release -o /app/publish --no-restore

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AsuntosService.dll"]
