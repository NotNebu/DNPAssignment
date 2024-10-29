using System.Net;
using System.Text.Json;

namespace WebAPI.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor to initialize ExceptionHandlingMiddleware.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Method to handle exceptions asynchronously.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Executes the next middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handles the exception asynchronously and returns an error message
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Method to handle the exception asynchronously.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Sets the content type to JSON
            context.Response.ContentType = "application/json";
            
            // Sets the status code to InternalServerError
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Creates a new response object
            var response = new
            {
                // Sets the status code
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred. Please try again later.",
                Detailed = exception.Message
            };

            // Serializes the response object to JSON
            var jsonResponse = JsonSerializer.Serialize(response);
            
            // Returns the JSON response
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}