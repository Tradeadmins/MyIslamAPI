using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System;
using MyIslamWebApp.Providers;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using Stripe;

[assembly: OwinStartup(typeof(MyIslamWebApp.Startup))]
namespace MyIslamWebApp
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        { 
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);
            
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["stripeSecretKey"]);
        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(7),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
    public class AssignOAuth2SecurityRequirements : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            // Determine if the operation has the Authorize attribute
            var authorizeAttributes = apiDescription
                .ActionDescriptor.GetCustomAttributes<AuthorizeAttribute>();

            if (!authorizeAttributes.Any())
                return;

            // Correspond each "Authorize" role to an oauth2 scope
            var scopes =
                authorizeAttributes
                .SelectMany(attr => attr.Roles.Split(','))
                .Distinct()
                .ToList();

            // Initialize the operation.security property if it hasn't already been
            if (operation.security == null)
                operation.security = new List<IDictionary<string, IEnumerable<string>>>();

            var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", scopes }
                };

            operation.security.Add(oAuthRequirements);
        }
    }
}