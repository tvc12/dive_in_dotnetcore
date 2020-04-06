using System;
using System.Data.Common;
using System.Threading.Tasks;
using CatBasicExample.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace CatBasicExample.Exception
{
    public interface IExceptionHandler
    {
        public Task Invoke(HttpContext context);
    }
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment env;

        public ExceptionHandler(RequestDelegate next, IHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }



        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }


        public static async Task HandleExceptionAsync(HttpContext context, System.Exception ex, IHostEnvironment env)
        {
            BaseException exception = GetOrConvertToBaseException(ex);
            if (env.IsStaging())
            {
                // TODO: mode stating do something here
            }
            if (env.IsProduction())
            {
                // TODO: mode production do something here
            }
            String json = JsonUtils.ToJson(exception);
            await context.Response.WriteAsync(json);
        }

        private static BaseException GetOrConvertToBaseException(System.Exception ex)
        {
            switch (ex)
            {
                case BaseException e when ex is BaseException:
                    return e;

                case Npgsql.NpgsqlException exception when ex is Npgsql.NpgsqlException:
                    return new BaseException("Database error", "503", "service_unavailabel");

                default:
                    return new BaseException("Internal Server Error");
            }
        }


    }
}