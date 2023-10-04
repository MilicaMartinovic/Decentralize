using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace TestOrders.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case ArgumentNullException _:
                    code = HttpStatusCode.BadRequest;
                    result = exception.Message;
                    break;
                case HttpRequestException _:
                    code = HttpStatusCode.BadGateway;
                    result = "External service request failed";
                    break;
                case DbUpdateException _:
                    code = HttpStatusCode.InternalServerError;
                    result = "Database update failed";
                    break;
                case FileNotFoundException _:
                    code = HttpStatusCode.NotFound;
                    result = "File not found";
                    break;
                case IOException _:
                    code = HttpStatusCode.InternalServerError;
                    result = "I/O operation failed";
                    break;
                case TaskCanceledException _:
                    code = HttpStatusCode.RequestTimeout;
                    result = "Operation timed out";
                    break;
                case AggregateException _:
                    code = HttpStatusCode.InternalServerError;
                    result = "Multiple errors occurred";
                    break;

                default:
                    result = "Internal server error";
                    break;
            }

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            return response.WriteAsync(JsonConvert.SerializeObject(new { error = result }));
        }
    }
}
