using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Utility.Exceptions;
using Utility.Model;

namespace University_Api.MIddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public IWebHostEnvironment _env { get; }

        public ExceptionMiddleware(RequestDelegate next,IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsynce(context,ex,_env);
              
            }   
        }

        private async Task HandleExceptionAsynce(HttpContext context, Exception exception, IWebHostEnvironment _env)
        {
            var code = HttpStatusCode.InternalServerError;
            var errors = new ApiErrorResponse() 
            { 
                StatusCode = (int) code
            };
            if (_env.IsDevelopment())
            {
                errors.Detailes = exception.StackTrace;
            }
            else
            {
                errors.Detailes = exception.Message;
            }

            switch (exception)
            {
                case ApplicationException e:
                    errors.Message = e.Message;
                    errors.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                default:
                    errors.Message = "Somnething is wrong in our systam";
                    break;
            }
            var result = JsonConvert.SerializeObject(errors);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errors.StatusCode;
            await context.Response.WriteAsync(result);
        }
    }
}
