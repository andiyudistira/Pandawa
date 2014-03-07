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
using Siska.Data;
using Siska.Data.Dao;

namespace Brightstar.Data.Test.BrightstarHelper
{
    public abstract class BrightstarUnitTest
    {
        protected IWindsorContainer container;

        public BrightstarUnitTest()
        {
            container = new WindsorContainer();

            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.Log4net).WithAppConfig());
            log4net.Config.XmlConfigurator.Configure();

            container.Register(Component.For<DaoInterceptor>());

            container.Register(Component.For<ISiskaDB>().ImplementedBy<SiskaDB>().LifeStyle.Singleton);

            AssemblyFilter filter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            if (System.Configuration.ConfigurationManager.AppSettings["DaoAssemblyName"].ToString().Equals("Siska.Data.Brightstar"))
            {
                container.Register(Classes.FromAssemblyInDirectory(filter)
                          .Where(c => c.IsClass &&
                                        (c.Name.Contains("Dao")) && c.Assembly.GetName().Name != "Siska.Data.NHibernate"
                                        && c.Assembly.GetName().Name != "Siska.Data.NDatabase")
                          .WithService.DefaultInterfaces()
                          .LifestyleSingleton()
                          .Configure(delegate(ComponentRegistration c) { var x = c.Interceptors(InterceptorReference.ForType<DaoInterceptor>()).Anywhere; })
                        );
            }
        }
    }
}
