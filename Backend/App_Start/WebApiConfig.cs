using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Backend
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

            ConfigureDataFormatters(config);
        }

        public static void ConfigureDataFormatters(HttpConfiguration config)
        {
            //config.Filters.Add(new ElmahErrorAttribute());

            // disable xml serialization
            config.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            var jsonSetting = new JsonSerializerSettings();

            jsonSetting.Converters.Add(new StringEnumConverter());
            jsonSetting.Formatting = Formatting.Indented;
            jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSetting.NullValueHandling = NullValueHandling.Include;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jsonSetting;
        }
    }
}
