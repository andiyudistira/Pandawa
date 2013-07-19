using System;
using Castle.Facilities.AutoTx;
using Castle.Facilities.AutoTx.Testing;
using Castle.Facilities.NHibernate;
using Castle.MicroKernel.Registration;
using Castle.Transactions;
using Castle.Windsor;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;

using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using Castle.Facilities.Logging;
using NHibernate;

namespace POS.Data.Test
{
    public class NHInstaller : INHibernateInstaller
    {
        public bool IsDefault
        {
            get { return true; }
        }

        public string SessionFactoryKey
        {
            get { return "def"; }
        }

        public Maybe<IInterceptor> Interceptor
        {
            get { return Maybe.None<IInterceptor>(); }
        }

        public FluentConfiguration BuildFluent()
        {
            var cfg = new Configuration().Configure();

            FluentConfiguration fc = Fluently.Configure(cfg)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Siska.Data.NHibernate.Dao.HibernateDao>());

            //SchemaExport schemaExport = new SchemaExport(fc);
            //schemaExport.Execute(false, true, false);

            return fc;
        }

        public void Registered(NHibernate.ISessionFactory factory)
        {
        }
    }
}
