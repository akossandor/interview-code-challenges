using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;

namespace OneBeyondApi.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error occurred");
                await WriteResponse(httpContext, exception);
            }
        }

        // Helpers

        private async Task WriteResponse(HttpContext httpContext, Exception exception)
        {
            var response = httpContext.Response;
            response.ContentType = MediaTypeNames.Application.Json;

            switch (exception)
            {
                case BadHttpRequestException badRequestException:
                    response.StatusCode = badRequestException.StatusCode;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonConvert.SerializeObject(new { message = exception?.Message });
            await response.WriteAsync(result);
        }
    }
}
