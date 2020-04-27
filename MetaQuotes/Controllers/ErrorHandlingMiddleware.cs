using MetaQuotes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaQuotes.Controllers
{
    public class ErrorHandlingMiddleware
    {
        public enum UiErrorType {
            UNKNOWN = 0,
            INCORRECT_IP=1
        }



        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            UiErrorType code = UiErrorType.UNKNOWN;
            if (ex is IncorrectIPFormatException)
            {
                code = UiErrorType.INCORRECT_IP;
            }


            var result = JsonConvert.SerializeObject(new { error = ex.Message, code = 1 });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            return context.Response.WriteAsync(result);
        }
    }
}
