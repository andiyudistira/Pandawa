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
            container.Register(
                Classes.FromAssemblyNamed(System.Configuration.ConfigurationManager.AppSettings["AssemblyName"].ToString())
                    .Where(c => c.IsClass && c.Name.Contains("Service"))
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient());

            container.Register(
                Classes.FromAssemblyNamed(System.Configuration.ConfigurationManager.AppSettings["ServiceAssemblyName"].ToString())
                      .Where(c => c.IsClass && c.Name.Contains("Service"))
                      .WithService.DefaultInterfaces()
                      .LifestyleSingleton()
                      //.Configure(delegate(ComponentRegistration c) { var x = c.Interceptors(InterceptorReference.ForType<DaoInterceptor>()).Anywhere; })
                    ); 
        }
    }
}
