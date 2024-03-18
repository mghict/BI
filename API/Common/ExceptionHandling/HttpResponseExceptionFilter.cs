using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.ExceptionHandling;

namespace API.Common.ExceptionHandling
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        private readonly IHostEnvironment hostEnvironment;

        public int Order => int.MaxValue - 10;

        public HttpResponseExceptionFilter(
            IHostEnvironment hostEnvironment
            )
        {
            this.hostEnvironment = hostEnvironment;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                object response;
                var exp = context.Exception;

                if (exp is BizException)
                {
                    var bizException = (BizException)context.Exception;
                    response = new
                    {
                        code = bizException.Code,
                        message = string.IsNullOrWhiteSpace(bizException.Message) ? "هنگام پردازش خطای غیر منتظره ای رخ داد است" : bizException.Code,
                        //message = bizException.Code ?? "هنگام پردازش خطای غیر منتظره ای رخ داد است"
                    };
                }
                else
                {
                    var isDev = hostEnvironment.IsDevelopment() || hostEnvironment.IsStaging();
                    response = new
                    {
                        code = -1,
                        message = exp.Message,
                        innerError = exp.Message,
                    };
                }

                context.Result = new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
