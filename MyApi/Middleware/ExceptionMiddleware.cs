using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyApp.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Llama al siguiente middleware en el pipeline
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Loguea la excepción para rastrear errores
            _logger.LogError(exception, "An error occurred");

            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = StatusCodes.Status500InternalServerError;
            var message = "Ocurrió un error inesperado.";

            // Mapeo de excepciones a mensajes claros
            if (exception is KeyNotFoundException keyNotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
                message = MapKeyNotFoundMessage(keyNotFoundException.Message);
            }
            else if (exception is ArgumentNullException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = "El argumento proporcionado es nulo.";
            }
            else if (exception is ArgumentException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = "El id proporcionado no coincide con el id de la URL.";
            }
            else if (exception is InvalidOperationException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = "La operación no es válida en este contexto.";
            }

            // Crea la respuesta con el mensaje y el código de estado
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(new
            {
                statusCode,
                message
            }.ToString());
        }

        // Método para mapear mensajes de KeyNotFoundException
        private string MapKeyNotFoundMessage(string exceptionMessage)
        {
            // Mapeamos los mensajes relacionados con el id para crear el mensaje dinámico
            if (exceptionMessage.Contains("id"))
            {
                var match = Regex.Match(exceptionMessage, @"(\d+)");  // Extraemos el id de la excepción
                if (match.Success)
                {
                    var id = match.Value;
                    return $"Cliente con id {id} no encontrado.";
                }
            }

            // Si no encontramos el id, devolvemos un mensaje genérico
            return "Elemento no encontrado.";
        }

        // Método para mapear el mensaje de ArgumentException
        private string MapArgumentExceptionMessage(string exceptionMessage)
        {
            // En este caso, el mensaje de ArgumentException tiene que ver con el id
            if (exceptionMessage.Contains("id"))
            {
                var match = Regex.Match(exceptionMessage, @"(\d+)");  // Extraemos el id de la excepción
                if (match.Success)
                {
                    var id = match.Value;
                    return $"El id proporcionado no coincide con el id de la URL ({id}).";
                }
            }

            // Si no podemos mapearlo, devolvemos un mensaje genérico
            return "El argumento proporcionado es inválido.";
        }
    }
}
