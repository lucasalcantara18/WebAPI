using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using WebAPI.Controllers;

namespace WebAPI.Modules.Filters
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<ApiController>>();
            var request = context.HttpContext.Request;

            logger.LogInformation("WebApi controller: Rota do tipo {arg1} acionada: `{arg2}://{arg3}{arg4}`", request.Method, request.Scheme, request.Host, request.Path);

            if (request.ContentLength != null && request.ContentLength > 0 && !request.HasFormContentType && request.Body.CanSeek)
            {
                var reader = new StreamReader(request.Body);
                reader.BaseStream.Position = 0;
                var body = reader.ReadToEndAsync().Result;

                var json = JToken.Parse(body).ToString(Formatting.Indented);
                json = RemoveSenhaDoJson(json);
                logger.LogInformation("WebApi controller data: Body da requisição: \n{arg1}", json);
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
            }
            

            base.OnActionExecuting(context);
        }

        private static string? RemoveSenhaDoJson(string json)
        {
            try
            {
                var o = JsonConvert.DeserializeObject(json) as JObject;
                var campoSenha = o?.Property("senha");
                if (campoSenha != null)
                {
                    campoSenha.Remove();
                }

                return o?.ToString();
            }
            catch (Exception)
            {
                return json;
            }
        }
    }
}