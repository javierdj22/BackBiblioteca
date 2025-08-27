using Microsoft.EntityFrameworkCore;
using MyApp.API.Configuration;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configuración de servicios

// Registrar el DbContext para SQL Server
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Resto de servicios personalizados (tus métodos de extensión)
builder.Services.AddAppServices(builder.Configuration);

// 🔹 Configuración de Swagger
builder.Services.AddSwaggerConfig();

// 🔹 Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// 🔹 Configuración de autenticación JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

// 🔹 Configuración de autorización
builder.Services.AddAuthorization();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();

var app = builder.Build();

// 🔹 Middleware
//app.UseHttpsRedirection();

// 🔹 Manejo global de errores
app.UseMiddleware<ExceptionMiddleware>();

// 🔹 CORS
app.UseCors("AllowReactApp");
app.UseWhen(context => context.Request.Method == "OPTIONS", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.StatusCode = 200;
        await Task.CompletedTask;
    });
});

// 🔹 Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Swagger siempre habilitado
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API Local v1");
    c.RoutePrefix = "swagger"; // o usa string.Empty si quieres que esté en la raíz
});

// 🔹 Mapear controladores
app.MapControllers();

// 🔹 Ejecutar la aplicación
app.Run();
