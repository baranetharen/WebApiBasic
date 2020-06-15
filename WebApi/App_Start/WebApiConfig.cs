using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Microsoft.Web.Http;
using Newtonsoft.Json.Serialization;
using Microsoft.Web.Http.Versioning;
using System.Web.Http.Routing;
using Microsoft.Web.Http.Routing;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            AutofacConfig.Register();


            config.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 1);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                //opt.ApiVersionReader = ApiVersionReader.Combine
                //(new HeaderApiVersionReader("X-Version"), new QueryStringApiVersionReader("ver"));

                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            });


            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver
                = new CamelCasePropertyNamesContractResolver();



            var contractResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap = { ["apiVersion"] = typeof(ApiVersionRouteConstraint) }
            };

            config.MapHttpAttributeRoutes(contractResolver);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
