using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Siska.Core;
using Siska.Data.Dao;

namespace NDatabase.Data.Test
{
    [TestClass]
    public class BaseDaoTest : BaseDao
    {
        [TestMethod]
        public void BaseDaoTestEquals()
        {
            List<CriteriaParam> param = new List<CriteriaParam>();
            
            param.Add(new CriteriaParam() { FieldName = "UserName", Operator = Operators.Equals, Value = "andi" });
            param.Add(new CriteriaParam() { FieldName = "Password", Operator = Operators.Equals, Value = "testing" });

            string queryFilter = this.CreateCriteria(param);

            Assert.IsNotNull(queryFilter);
        }
    }
}
