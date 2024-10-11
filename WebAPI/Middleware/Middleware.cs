using System.Net;
using System.Text.Json;

namespace WebAPI.Middleware
{
    // Middleware til at håndtere exceptions
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        // Constructor til at initialisere ExceptionHandlingMiddleware
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Metode til at håndtere exceptions asynkront
        public async Task InvokeAsync(HttpContext context)
        {
            // Prøver at køre næste middleware
            try
            {
                // Kører næste middleware
                await _next(context);
            }
            // Håndterer exception
            catch (Exception ex)
            {
                // Håndterer exception asynkront og returnerer en fejlbesked
                await HandleExceptionAsync(context, ex);
            }
        }

        // Metode til at håndtere exception asynkront
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Sætter content type til JSON
            context.Response.ContentType = "application/json";
            
            // Sætter statuskode til InternalServerError
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Opretter et nyt response-objekt
            var response = new
            {
                // Sætter statuskode
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred. Please try again later.",
                Detailed = exception.Message
            };

            // Serializerer response-objektet til JSON
            var jsonResponse = JsonSerializer.Serialize(response);
            
            // Returnerer JSON-responsen
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}