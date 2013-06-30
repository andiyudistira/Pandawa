﻿using System;
using NHibernate;
using NHibernate.Cfg;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System.IO;
using Siska.Data.NHibernate;
using Castle.Facilities.NHibernate;
using NLog;
using Castle.Facilities.AutoTx;
using Castle.Facilities.Logging;
using Siska.Data;
using Castle.Core;

namespace POS.Data.Test
{
    public abstract class NHUnitTest
    {
        protected ISessionFactory sessionFactory;
        protected Configuration configuration;
        protected IWindsorContainer container;

        public NHUnitTest()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            container = new WindsorContainer();
            
            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.Log4net).WithAppConfig());
            container.Register(Component.For<DaoInterceptor>());

            container
                .AddFacility<AutoTxFacility>()
                .Register(
                    Component.For<INHibernateInstaller>().ImplementedBy<NHInstaller>().LifeStyle.Singleton,
                    Component.For<Logger>().LifeStyle.Singleton)
                    .AddFacility<NHibernateFacility>();

            AssemblyFilter filter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            container.Register(Classes.FromAssemblyInDirectory(filter)
                                  .Where(c => c.IsClass &&
                                                (c.Name.Contains("Dao")) && c.Assembly.GetName().Name != "Siska.Data.NDatabase")
                                  .WithService.DefaultInterfaces()
                                  .LifestyleSingleton()
                                  .Configure(delegate(ComponentRegistration c) { var x = c.Interceptors(InterceptorReference.ForType<DaoInterceptor>()).Anywhere; })
                                );

            log4net.Config.XmlConfigurator.Configure();
        }

        protected T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        protected void Resolve(object _object)
        {
            container.Release(_object);
        }
    }
}