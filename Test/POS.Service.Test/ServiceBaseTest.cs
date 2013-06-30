using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Siska.Service;

namespace POS.Service.Test
{
    public class ServiceBaseTest
    {
        protected IWindsorContainer container;

        public ServiceBaseTest()
        {
            container = new WindsorContainer();

            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.Log4net).WithAppConfig());

            container.Register(Component.For<ServiceInterceptor>());

            log4net.Config.XmlConfigurator.Configure();
        }

        protected T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        protected void Resolve(object _object)
        {
            container.Release(_object);
        }
    }
}
