using System.Web.Http;
using WebActivatorEx;
using WebApp.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApp.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
               .EnableSwagger(c => c.SingleApiVersion("v1", "RXN"))
               .EnableSwaggerUi();
        }
    }
}
