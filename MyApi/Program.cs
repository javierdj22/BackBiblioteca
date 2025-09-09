using MyApp.API.Configuration;
using MyApp.API.Middlewares;
using MyApp.Application.Interfaces;
using MyApp.Application.DependencyInjection;
using MyApp.Application.Services;
using MyApp.Infrastructure.Intranet.Dependencia;


var builder = WebApplication.CreateBuilder(args);

// Resto de servicios personalizados (tus métodos de extensión)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

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

// Agregar el middleware de excepciones al pipeline
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Registro del TokenService con la clave
var secretKey = builder.Configuration["JwtSettings:SecretKey"];
builder.Services.AddSingleton<ITokenService>(new TokenService(secretKey));

// 🔹 Configuración de autenticación JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

// 🔹 Configuración de autorización
builder.Services.AddAuthorization();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();
;

var app = builder.Build();


// Usa el middleware para manejar las excepciones
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
