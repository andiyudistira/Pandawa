using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siska.Data.Dao;

namespace POS.Data.Test
{
    [TestClass]
    public class UserSessionDaoTest : NHUnitTest
    {
        private IUserSessionDao UserSessionDao
        {
            get { return Resolve<IUserSessionDao>(); }
        }

        [TestMethod]
        public void SanityCheck()
        {
            Assert.IsNotNull(UserSessionDao, "User dao is null");
        }

        [TestMethod]
        public void RecordFetchTest()
        {
            // Test Fetch all without parameter
            var userSessionDt = UserSessionDao.GetAll();

            // Test Fetch all with parameter
            //List<CriteriaParam> param = new List<CriteriaParam>();

            //param.Add(new CriteriaParam() { FieldName = "UserName", Operator = Operators.Or, Value = "", Left = leftParam, Right = rightParam });
            //param.Add(new CriteriaParam() { FieldName = "UserName", Operator = Operators.Equals, Value = "andi" });

            //var userTest = UserDao.GetByCriteria(param);
        }
    }
}
