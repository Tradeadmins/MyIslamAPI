using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using MyIslamWebApp.Filter;
using Swashbuckle.Application;
using System;

namespace MyIslamWebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();
           

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            config.MessageHandlers.Add(new CustomResponseHandler());

            config
           .EnableSwagger(c =>
           {
               c.SingleApiVersion("v1", "My Islam Apis");
               c.IncludeXmlComments(string.Format(@"{0}\bin\MyIslamWebApp.XML", AppDomain.CurrentDomain.BaseDirectory));
               c.DescribeAllEnumsAsStrings();
               c.OAuth2("oauth2")
                      .Description("OAuth2 Password Grant")
                      .Flow("password")
                      .TokenUrl("http://103.73.190.66:8080/token")
                      .Scopes(scopes =>
                      {
                          scopes.Add("read", "Read access to protected resources");
                          scopes.Add("write", "Write access to protected resources");
                      });
               c.OperationFilter<AssignOAuth2SecurityRequirements>();
           })
           .EnableSwaggerUi();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
