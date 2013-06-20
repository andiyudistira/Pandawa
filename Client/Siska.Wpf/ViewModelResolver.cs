using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Siska.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Siska.Wpf
{
    public class ViewModelResolver : IViewModelResolver
    {
        private IWindsorContainer container = null;

        public object Resolve(string viewModelName)
        {
            if (container == null)
            {
                container = new WindsorContainer();
                container.Install(new WindsorServiceInstaller());
                container.Install(new WindsorViewsInstaller());
            }

            var gg = Assembly.LoadFrom(System.Configuration.ConfigurationManager.AppSettings["MainAssembly"].ToString());
            var viewModelType = gg.GetTypes().Where(t => t.Name.Equals(viewModelName)).FirstOrDefault();

            //var viewModelType =
            //    this.GetType()
            //    .Assembly
            //    .GetTypes()
            //    .Where(t => t.Name.Equals(viewModelName))
            //    .FirstOrDefault();

            return container.Resolve(viewModelType);
        }
    }
}
