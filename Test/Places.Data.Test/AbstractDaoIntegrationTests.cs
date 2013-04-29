using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Testing.Microsoft;

namespace Places.Data.Test
{
    /// <summary>
    /// This class contains tests for 
    /// </summary>
    [TestClass]
    public abstract class AbstractDaoIntegrationTests : AbstractTransactionalDbProviderSpringContextTests
    {
        protected override string[] ConfigLocations
        {
            get
            {
                return new string[]
                    {
                        "assembly://Siska.Data.NHibernate/Siska.Data.NHibernate.Dao/Dao.xml",
                    };
            }
        }
    }
}
