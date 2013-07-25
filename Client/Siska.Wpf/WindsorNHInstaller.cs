namespace Siska.Wpf
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Castle.Facilities.AutoTx;
    using Castle.Facilities.Logging;
    using Castle.Facilities.NHibernate;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using log4net.Repository.Hierarchy;
    using NHibernate.Validator.Cfg;
    using NHibernate.Validator.Engine;
    using NHibernate.Validator.Event;
    using Siska.Data;

    public class WindsorNHInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var config = new XmlConfiguration();

            config.Properties[Environment.ApplyToDDL] = "false";
            config.Properties[Environment.AutoregisterListeners] = "true";
            config.Properties[Environment.ValidatorMode] = "UseAttribute";
            config.Properties[Environment.SharedEngineClass] = typeof(ValidatorEngine).FullName;
            config.Mappings.Add(new MappingConfiguration(System.Configuration.ConfigurationManager.AppSettings["AssemblyName"].ToString(), null));

            Environment.SharedEngineProvider = new NHibernateSharedEngineProvider();
            Environment.SharedEngineProvider.GetEngine().Configure(config);

            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.Log4net).WithAppConfig());
            log4net.Config.XmlConfigurator.Configure();

            container.Register(Component.For<DaoInterceptor>());

            container
                .AddFacility<AutoTxFacility>()
                .Register(
                    Component.For<INHibernateInstaller>().ImplementedBy<NHInstaller>().LifeStyle.Singleton,
                    Component.For<Logger>().LifeStyle.Singleton)
                    .AddFacility<NHibernateFacility>();

            //ValidatorInitializer.Initialize(NHibernateConfig);
        }
    }
}
