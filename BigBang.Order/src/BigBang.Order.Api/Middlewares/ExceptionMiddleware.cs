using BigBang.Order.Infrastructure.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BigBang.Order.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            var response = httpContext.Response;

            var errorModel = new ErrorModel();

            switch (exception)
            {
                
                case HttpRequestException httpRequestException:
                    response.StatusCode = (int)httpRequestException.StatusCode;
                    errorModel.Message = httpRequestException.Message;
                    break;
                case TimeoutException timeoutException:
                    response.StatusCode = (int)HttpStatusCode.GatewayTimeout;
                    errorModel.Message = timeoutException.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorModel.Message = "Internal Server Error";
                    break;
            }

            var result = JsonConvert.SerializeObject(errorModel);

            await httpContext.Response.WriteAsync(result);
        }
    }
}
