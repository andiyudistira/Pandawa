using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Siska.Core;
using Siska.Data.NHibernate.Dao;
using Siska.Data.Dao;

namespace Places.Data.Test.Sqlite
{
    [TestClass]
    public class AccCategoryTest : NHUnitTest
    {
        // These properties will be injected based on type
        private ICategoryDao CategoryDao
        {
            get { return Resolve<ICategoryDao>(); }
        }

        private IAccountDao AccountDao
        {
            get { return Resolve<IAccountDao>(); }
        }  

        //[TestMethod]
        public void SanityCheck()
        {
            Assert.IsNotNull(CategoryDao, "area dao is null");
        }

        [TestMethod]
        public void GetAllTest()
        {
            var accountDt = AccountDao.GetAll();

            // Test the Dao
            var categoryDt = CategoryDao.GetAll();

            var categoryDt2 = CategoryDao.GetAll();

            // Test the 2nd

            Assert.IsNotNull(categoryDt);

            // Test branch
            Assert.IsTrue(categoryDt.Count > 0);
        }

        //[TestMethod]
        public void GetAllByCriteria()
        {
           //List<CriteriaParam> param = new List<CriteriaParam>();

           //CriteriaParam leftParam = new CriteriaParam() { FieldName = "AreaCode", Operator = Operators.Equals, Value = "dewa" };
           //CriteriaParam rightParam = new CriteriaParam() { FieldName = "AreaCode", Operator = Operators.Equals, Value = "andi" };

           //param.Add(new CriteriaParam() { FieldName = "", Operator = Operators.Or, Value = "", Left = leftParam, Right = rightParam });
           //param.Add(new CriteriaParam() { FieldName = "Description", Operator = Operators.Equals, Value = "123123" });

           //var areaDt = CategoryDao.GetByCriteria(param);
        }

        //[TestMethod]
        public void GetAllWithPaging()
        {
           //int totalPage = 0;

           //var page1 = AreaDao.GetAll(0, 2, out totalPage);
           //var page2 = AreaDao.GetAll(1, 2, out totalPage);
           //var page3 = AreaDao.GetAll(2, 2, out totalPage);
           //var page4 = AreaDao.GetAll(3, 2, out totalPage);
        }

        //[TestMethod]
        public void GetByIdTest()
        {
           //var areaDt = AreaDao.Get(1);

           //Assert.IsNotNull(areaDt);
           //Assert.IsTrue(areaDt.AreaId == 1);
        }
    }
}
