using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GalaSoft.MvvmLight;
using System;
using System.IO;

namespace Siska.Wpf
{
    public class WindsorViewsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AssemblyFilter filter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            container.Register(
                Classes.FromAssemblyNamed(System.Configuration.ConfigurationManager.AppSettings["AssemblyName"].ToString())
                    .BasedOn<ViewModelBase>()
                    .LifestyleTransient());
        }
    }
}
