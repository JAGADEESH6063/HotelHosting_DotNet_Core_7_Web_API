using Newtonsoft.Json;
using System.Net;

namespace HotelHosting.ExceptionMiddleWare
    {
    public class ExceptionMiddleWare
        {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger)
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
                _logger.LogError(ex, $"something went wrong while processing: {context.Request.Path}");
                await HandleExceptionAsync(context, ex);
                }
            }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
            {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorDetails
                {
                ErrorType = "Failure",
                ErrorMessage = ex.Message,
                };
            switch (ex)
                {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorDetails.ErrorType = "Not Found";
                    break;
                default:
                    break;
                }
            string reponse = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(reponse);
            }
        }
    public class ErrorDetails
        {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        }
    }
