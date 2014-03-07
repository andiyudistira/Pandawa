namespace Siska.Web
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using System.Web.Mvc;

    public class WindsorControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyNamed(System.Configuration.ConfigurationManager.AppSettings["AssemblyName"].ToString())
                                .BasedOn<IController>()
                                .LifestyleTransient());
        }
    }

}
