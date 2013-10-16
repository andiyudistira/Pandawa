namespace Siska.Data.Dao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Siska.Core;

    public abstract class BaseDao
    {
        protected string CreateCriteria<T>(List<CriteriaParam> itemParams)
        {
            StringBuilder criteriaResult = new StringBuilder(1000);

            int i = 0;
            foreach (CriteriaParam item in itemParams)
            {
                if (i > 0)
                {
                    criteriaResult.Append(" AND ");
                }

                if (item.Value.GetType().Equals(typeof(string)))
                {
                    item.Value = string.Format("{0}{1}{2}", "\"", item.Value, "\"");
                }

                if (item.Operator == Operators.Equals)
                {
                    criteriaResult.Append(string.Format("{0} {1} {2}", item.FieldName, "=", item.Value));
                }
                else if (item.Operator == Operators.In)
                {
                    criteriaResult.Append(string.Format("{0} {1} {2}", item.Value, item.Operator.ToString().ToUpper(), item.FieldName));
                }
                else if (item.Operator == Operators.Between)
                {
                    criteriaResult.Append(string.Format("{0} {1} {2} {3} {4}", item.FieldName, 
                        item.Operator.ToString().ToUpper(), item.ValueLo, "AND", item.ValueHigh));
                }
                else if (item.Operator == Operators.Like)
                {
                    criteriaResult.Append(string.Format("{0} {1} {2}", item.FieldName,
                        "CONTAINS", item.Value));
                }

                i++;
            }

            return criteriaResult.ToString();
        }
    }
}
