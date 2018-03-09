using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using dng.Syndication.Formatters;

namespace WebAppSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultFeed",
                routeTemplate: "feed/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = nameof(Controllers.SampleController),
                    id = RouteParameter.Optional
                }
            );

            config.Formatters.Add(new AtomMediaTypeFormatter());
            config.Formatters.Add(new RssMediaTypeFormatter());

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
