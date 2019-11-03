using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace Common.Api.Middlewares
{
    public sealed class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate Next;

        readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            Next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (System.Exception ex)
            {
                this._logger.Log(LogLevel.Error, ex.Message);

                await HandledExceptionAsync(context, ex);
            }
        }

        private static Task HandledExceptionAsync(HttpContext context, System.Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = JsonConvert.SerializeObject(new { messages = new string[] { exception.Message } });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

    }
}
