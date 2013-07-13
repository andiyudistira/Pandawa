
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Event;

namespace Siska.Wpf
{
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

            //ValidatorInitializer.Initialize(NHibernateConfig);
        }
    }
}
