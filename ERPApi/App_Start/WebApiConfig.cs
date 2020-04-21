﻿using System.Web.Http;
using System.Web.Http.Cors;

namespace ERPApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var enableCorsAttribute = new EnableCorsAttribute("http://59.152.97.62",
                                                  "Origin, Content-Type, Accept",
                                                  "GET, PUT, POST, DELETE, OPTIONS");
            config.EnableCors(enableCorsAttribute);

            config.Routes.MapHttpRoute(
                name: "ControllersWithAction",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}