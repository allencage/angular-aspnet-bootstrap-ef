using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace Web.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver =
              new CamelCasePropertyNamesContractResolver();

			config.Formatters.Remove(config.Formatters.XmlFormatter);

			config.Routes.MapHttpRoute(
                name: "RepliesApi",
                routeTemplate: "api/topics/{topicid}/replies/{id}",
                defaults: new { controller = "replies", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "TopicsApi",
                routeTemplate: "api/topics/{id}",
                defaults: new { controller = "topics", id = RouteParameter.Optional }
            );

            config.MapHttpAttributeRoutes();
        }
    }
}