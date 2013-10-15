namespace POS.Data.Test
{
    using Castle.Facilities.NHibernate;
    using Castle.Transactions;
    using FluentNHibernate.Cfg;
    using NHibernate;
    using NHibernate.Cfg;

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

            return Fluently.Configure(cfg)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Siska.Data.Dao.HibernateDao>());
        }

        public void Registered(NHibernate.ISessionFactory factory)
        {
        }
    }
}
