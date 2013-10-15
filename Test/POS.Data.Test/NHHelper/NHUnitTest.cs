using System;
using NHibernate;
using NHibernate.Cfg;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System.IO;
using Siska.Data;
using Castle.Facilities.NHibernate;
using NLog;
using Castle.Facilities.AutoTx;
using Castle.Facilities.Logging;
using Siska.Data;
using Castle.Core;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Context;
using Siska.Data.Dao.Pos;
using Siska.Data.Model.Pos;
using System.Collections.Generic;

namespace POS.Data.Test
{
    public abstract class NHUnitTest
    {
        protected ISessionFactory sessionFactory;
        protected Configuration configuration;
        protected IWindsorContainer container;
        private ISessionManager globalSessionManager;
        protected ISession globalSession;

        public NHUnitTest()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            container = new WindsorContainer();
            
            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.Log4net).WithAppConfig());
            log4net.Config.XmlConfigurator.Configure();

            container.Register(Component.For<DaoInterceptor>());

            container
                .AddFacility<AutoTxFacility>()
                .Register(
                    Component.For<INHibernateInstaller>().ImplementedBy<NHInstaller>().LifeStyle.Singleton,
                    Component.For<Logger>().LifeStyle.Singleton)
                    .AddFacility<NHibernateFacility>();

            ExportDatabaseSchema();

            AssemblyFilter filter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            container.Register(Classes.FromAssemblyInDirectory(filter)
                                  .Where(c => c.IsClass &&
                                                (c.Name.Contains("Dao")) && c.Assembly.GetName().Name != "Siska.Data.NDatabase")
                                  .WithService.DefaultInterfaces()
                                  .LifestyleSingleton()
                                  .Configure(delegate(ComponentRegistration c) { var x = c.Interceptors(InterceptorReference.ForType<DaoInterceptor>()).Anywhere; })
                                );            
        }

        protected T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        protected void Resolve(object _object)
        {
            container.Release(_object);
        }

        protected void ExportDatabaseSchema()
        {
            Configuration[] cfgs = container.ResolveAll<Configuration>();
            
            globalSessionManager = container.Resolve<ISessionManager>();

            globalSession = globalSessionManager.OpenSession();

            foreach (Configuration cfg in cfgs)
            {
                SchemaExport export = new SchemaExport(cfg);
                //export.Create(false, true);
                export.Execute(false, true, false, globalSession.Connection, null);
            }

            prepareTestData();

            //SchemaExport schemaExport = new SchemaExport(cfg);
            //schemaExport.SetOutputFile("C:\\MyDDL.sql");
            //schemaExport.Execute(true, true, false, getSession().Connection, null);
        }

        private void prepareTestData()
        {
            UserDao userDao = new UserDao(() => globalSession);
            RoleDao roleDao = new RoleDao(() => globalSession);
            UserSessionDao userSessionDao = new UserSessionDao(() => globalSession);

            #region Prepare User Data
            User userAndi = new User();
            userAndi.UserName = "andi";
            userAndi.Password = "password";
            userAndi.RecordStatus = true;
            userDao.Add(userAndi);

            User userAdmin = new User();
            userAdmin.UserName = "admin";
            userAdmin.Password = "password";
            userAdmin.RecordStatus = true;
            userDao.Add(userAdmin);

            User userTester = new User();
            userTester.UserName = "tester";
            userTester.Password = "password";
            userTester.RecordStatus = true;
            userDao.Add(userTester);
            #endregion

            #region Prepare Role Data
            Role roleAdmin = new Role();
            roleAdmin.RoleName = "admin";
            roleAdmin.RecordStatus = true;
            roleAdmin.InsertBy = userAdmin;
            roleAdmin.InsertDate = DateTime.Today;
            roleDao.Add(roleAdmin);

            Role roleOperator = new Role();
            roleOperator.RoleName = "operator";
            roleOperator.RecordStatus = true;
            roleOperator.InsertBy = userAdmin;
            roleOperator.InsertDate = DateTime.Today;
            roleDao.Add(roleOperator);

            Role roleStaff = new Role();
            roleStaff.RoleName = "staff";
            roleStaff.RecordStatus = true;
            roleStaff.InsertBy = userAdmin;
            roleStaff.InsertDate = DateTime.Today;
            roleDao.Add(roleStaff);

            Role roleCashier = new Role();
            roleCashier.RoleName = "cashier";
            roleCashier.RecordStatus = true;
            roleCashier.InsertBy = userAdmin;
            roleCashier.InsertDate = DateTime.Today;
            roleDao.Add(roleCashier);
            #endregion

            #region User Role Data
            userAndi.Roles = new List<Role>();
            userAndi.Roles.Add(roleAdmin);
            userAndi.Roles.Add(roleStaff);
            userDao.Update(userAndi);
            #endregion
        }
    }
}
