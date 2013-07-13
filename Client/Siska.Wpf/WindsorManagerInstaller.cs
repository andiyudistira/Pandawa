using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Siska.Wpf.Manager;

namespace Siska.Wpf
{
    public class WindsorManagerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed(System.Configuration.ConfigurationManager.AppSettings["AssemblyName"].ToString())
                            .Where(c => c.IsClass && c.Name.Contains("Manager"))
                            .WithService.DefaultInterfaces()
                            .LifestyleSingleton());
        }
    }
}
