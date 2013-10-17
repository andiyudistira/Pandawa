namespace Siska.Data.Dao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NDatabase.Api.Query;
    using Siska.Core;

    public abstract class BaseDao
    {
        protected IQuery NDBSodaQueryCriteria(List<CriteriaParam> itemParams, IQuery queryCriteria)
        {
            IQuery query = queryCriteria;

            int i = 0;
            foreach (CriteriaParam item in itemParams)
            {
                if (item.Operator == Operators.Equals)
                {
                    query.Descend(item.FieldName).Constrain(item.Value).Equal();
                }

                if (item.Operator == Operators.In)
                {
                    query.Descend(string.Format("{0}{1}", "_", item.FieldName.ToLower())).Constrain(item.Value).Contains();
                }

                if (item.Operator == Operators.Between)
                {
                    var queryGt = query.Descend(string.Format("{0}{1}", "_", item.FieldName.ToLower())).Constrain(item.Value).SizeGt();
                    query.Descend(string.Format("{0}{1}", "_", item.FieldName.ToLower())).Constrain(item.Value).SizeLt().And(queryGt);
                }

                if (item.Operator == Operators.Like)
                {
                    query.Descend(string.Format("{0}{1}", "_", item.FieldName.ToLower())).Constrain(item.Value).Like();
                }

                if (item.Operator == Operators.NotEquals)
                {
                    query.Descend(string.Format("{0}{1}", "_", item.FieldName.ToLower())).Constrain(item.Value).Not().Equal();
                }

                if (item.Operator == Operators.NotIn)
                {
                    query.Descend(string.Format("{0}{1}", "_", item.FieldName.ToLower())).Constrain(item.Value).Not().Contains();
                }

                if (item.Operator == Operators.Greater)
                {
                    query.Descend(string.Format("{0}{1}", "_", item.FieldName.ToLower())).Constrain(item.Value).SizeGe();
                }

                if (item.Operator == Operators.Greater)
                {
                    query.Descend(string.Format("{0}{1}", "_", item.FieldName.ToLower())).Constrain(item.Value).SizeGt();
                }

                if (item.Operator == Operators.Smaller)
                {
                    query.Descend(string.Format("{0}{1}", "_", item.FieldName.ToLower())).Constrain(item.Value).SizeLe();
                }

                if (item.Operator == Operators.SmallerThan)
                {
                    query.Descend(string.Format("{0}{1}", "_", item.FieldName.ToLower())).Constrain(item.Value).SizeLt();
                }
            }

            return query;
        }

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
