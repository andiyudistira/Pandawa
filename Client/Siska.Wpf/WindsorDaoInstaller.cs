namespace Siska.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Castle.Core;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Siska.Data;

    public class WindsorDaoInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AssemblyFilter filter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            container.Register(
                Classes.FromAssemblyNamed(System.Configuration.ConfigurationManager.AppSettings["DaoAssemblyName"].ToString())
                      .Where(c => c.IsClass &&
                                    (c.Name.Contains("Dao")) && c.Assembly.GetName().Name != "Siska.Data.NDatabase")
                      .WithService.DefaultInterfaces()
                      .LifestyleSingleton()
                      .Configure(delegate(ComponentRegistration c) { var x = c.Interceptors(InterceptorReference.ForType<DaoInterceptor>()).Anywhere; })
                    ); 
        }
    }
}
