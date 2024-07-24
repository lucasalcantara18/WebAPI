using Domain.Extensions;
using Domain.Exceptions;
using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebAPI.ViewModels.Errors;

namespace WebAPI.Modules.Common
{
    public static class ExceptionExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();
            var environment = app.ApplicationServices.GetRequiredService<IHostEnvironment>();

            app.UseGlobalExceptionHandler(configuration =>
            {
                configuration.ContentType = "application/json";

                configuration.InternalErrorSettings(environment, logger);
            });

            return app;
        }

        private static void InternalErrorSettings<TStartup>(this ExceptionHandlerConfiguration configuration, IHostEnvironment environment, ILogger<TStartup> logger)
        {
            configuration.ResponseBody((error, context) =>
            {
                context.AddCorsHeaders();
                var exception = new FullExceptionViewModel("A aplicação está temporariamente indisponível. Tente novamente mais tarde ou contate o suporte.");

                if (!environment.IsProduction())
                {
                    exception.InnerMessage = error.GetFullInnerException();
                    exception.StackTrace = error.ToFormatedStackTrace();
                }

                return exception.AsJson();
            });

            configuration.OnError((exception, httpContext) =>
            {
                logger.LogError("Erro não tratado ocorreu na aplicação: \nMensagem: {arg1} \nStackTrace: {arg2} \nInnerException: {arg3}", exception.Message, exception.StackTrace, exception.GetFullInnerException());
                return Task.CompletedTask;
            });
        }
    }
}