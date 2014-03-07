namespace Siska.Wpf
{
    using Castle.Core;
    using Castle.Facilities.Logging;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Siska.Data;
    using System;
    using System.IO;

    public class WindsorDaoInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AssemblyFilter filter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.Log4net).WithAppConfig());
            log4net.Config.XmlConfigurator.Configure();

            container.Register(Component.For<DaoInterceptor>());

            container.Register(Component.For<ISiskaDB>().ImplementedBy<SiskaDB>().LifeStyle.Singleton);

            container.Register(
                Classes.FromAssemblyNamed(System.Configuration.ConfigurationManager.AppSettings["DaoAssemblyName"].ToString())
                      .Where(c => c.IsClass && c.Name.Contains("Dao"))
                      .WithService.DefaultInterfaces()
                      .LifestyleSingleton()
                      .Configure(delegate(ComponentRegistration c) { var x = c.Interceptors(InterceptorReference.ForType<DaoInterceptor>()).Anywhere; })
                    ); 
        }
    }
}
