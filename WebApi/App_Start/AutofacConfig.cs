using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using AutoMapper.Configuration;
using WebApi.Models;

namespace WebApi
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var bldr = new ContainerBuilder();
            bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(bldr);
            var config = GlobalConfiguration.Configuration;
            var container = bldr.Build();
            bldr.RegisterWebApiFilterProvider(config);
            bldr.RegisterWebApiModelBinderProvider();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder bldr)
        {
            var config = new MapperConfiguration(cof =>
            {
                cof.AddProfile(new CampProfile());
                cof.AddProfile(new SpeakerProfile());
                cof.AddProfile(new TalkProfile());
            });

            bldr.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance();
            bldr.RegisterType<CampContext>().InstancePerRequest();
            bldr.RegisterType<CampRepository>().As<ICampRepository>().InstancePerRequest();
        }
    }
}