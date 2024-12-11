
using FacturaApi.Services;
using FacturaAPI.Middleware;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Configurar Swagger para aceptar la API Key
        builder.Services.AddSwaggerGen(options =>
        {
            // Agregar la configuración de seguridad para la API Key
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "x-api-key", 
                Type = SecuritySchemeType.ApiKey,
                Description = "API Key requerida para acceder a la API"
            });

            // Configuración de seguridad en cada operación
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    new string[] {}
                }
            });
        });

         builder.Services.AddScoped<IFacturaService, FacturaService>();

        var app = builder.Build();

        app.UseWhen(context => !context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
        {
            appBuilder.UseMiddleware<ApiKeyMiddleware>(); // Aplica el middleware de API Key
        });

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Factura API v1"));
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}


