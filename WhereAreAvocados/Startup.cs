﻿using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Owin;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using System.Threading.Tasks;
using System.Configuration;

namespace WhereAreAvocados
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var authority = ConfigurationManager.AppSettings["AUTHORITY"];
            var audience = ConfigurationManager.AppSettings["AUDIENCE"];

            var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
               authority + "/.well-known/openid-configuration",
               new OpenIdConnectConfigurationRetriever(),
               new HttpDocumentRetriever());
            var discoveryDocument = Task.Run(() => configurationManager.GetConfigurationAsync()).GetAwaiter().GetResult();

            app.UseJwtBearerAuthentication(
               new JwtBearerAuthenticationOptions
               {
                   AuthenticationMode = AuthenticationMode.Active,
                   TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidAudience = audience,
                       ValidIssuer = authority,
                       IssuerSigningKeyResolver = (token, securityToken, identifier, parameters) =>
                       {
                           return discoveryDocument.SigningKeys;
                       }
                   }
               });

            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

            app.UseWebApi(config);
        }
    }
}