using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Siska.Wpf
{
    public class WindsorServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AssemblyFilter filter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            container.Register(
                Classes.FromAssemblyNamed(System.Configuration.ConfigurationManager.AppSettings["AssemblyName"].ToString())
                    .Where(c => c.IsClass && c.Name.Contains("Service"))
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient());

        }
    }
}
