using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using AsuntoService.Data;
using System;
using DotNetEnv;  // Importar la librería para cargar .env

public class Program
{
    public static void Main(string[] args)
    {
        // Cargar variables de entorno desde .env al iniciar la aplicación
        Env.Load();
        // Obtener la conexión desde variables de entorno
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION") ?? "Host=localhost;Port=5432;Database=asuntos_db;Username=postgres;Password=supersecreto";

        var builder = WebApplication.CreateBuilder(args);

        //Agregar el contexto de base de datos
        builder.Services.AddDbContext<AsuntoContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Agregar servicios al contenedor
        builder.Services.AddControllers(); // Permite el uso de controladores
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("NuevaPolitica", app =>
            {
                app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configurar pipeline de HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers(); // Mapear controladores
        //politica de CORS
        app.UseCors("NuevaPolitica");

        app.Run();
    }
}
