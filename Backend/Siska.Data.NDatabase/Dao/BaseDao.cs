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
                    if (i > 0)
                    {
                        query.Descend(item.FieldName).Constrain(item.Value).Equal();
                    }
                    else
                    {
                        var q = query.Constrain(null);
                        query.Descend(item.FieldName).Constrain(item.Value).Equal().And(q);
                    }
                }

                if (item.Operator == Operators.In)
                {
                    if (i > 0)
                    {
                        query.Descend(item.FieldName).Constrain(item.Value).Contains();
                    }
                    else
                    {
                        var q = query.Constrain(null);
                        query.Descend(item.FieldName).Constrain(item.Value).Contains().And(q);
                    }
                }

                if (item.Operator == Operators.Between)
                {
                    if (i > 0)
                    {
                        var queryGt = query.Descend(item.FieldName).Constrain(item.Value).SizeGt();
                        query.Descend(item.FieldName).Constrain(item.Value).SizeLt().And(queryGt);
                    }
                    else
                    {
                        var q = query.Constrain(null);
                        var queryGt = query.Descend(item.FieldName).Constrain(item.Value).SizeGt();
                        query.Descend(item.FieldName).Constrain(item.Value).SizeLt().And(queryGt).And(q);
                    }
                }

                if (item.Operator == Operators.Like)
                {
                    if (i > 0)
                    {
                        query.Descend(item.FieldName).Constrain(item.Value).Like();
                    }
                    else
                    {
                        var q = query.Constrain(null);
                        query.Descend(item.FieldName).Constrain(item.Value).Like().And(q);
                    }
                }

                if (item.Operator == Operators.NotEquals)
                {
                    if (i > 0)
                    {
                        query.Descend(item.FieldName).Constrain(item.Value).Not().Equal();
                    }
                    else
                    {
                        var q = query.Constrain(null);
                        query.Descend(item.FieldName).Constrain(item.Value).Not().Equal().And(q);
                    }
                }

                if (item.Operator == Operators.NotIn)
                {
                    if (i > 0)
                    {
                        query.Descend(item.FieldName).Constrain(item.Value).Not().Contains();
                    }
                    else
                    {
                        var q = query.Constrain(null);
                        query.Descend(item.FieldName).Constrain(item.Value).Not().Contains().And(q);
                    }
                }

                if (item.Operator == Operators.Greater)
                {
                    if (i > 0)
                    {
                        query.Descend(item.FieldName).Constrain(item.Value).SizeGe();
                    }
                    else
                    {
                        var q = query.Constrain(null);
                        query.Descend(item.FieldName).Constrain(item.Value).SizeGe().And(q);
                    }
                }

                if (item.Operator == Operators.Greater)
                {
                    if (i > 0)
                    {
                        query.Descend(item.FieldName).Constrain(item.Value).SizeGt();
                    }
                    else
                    {
                        var q = query.Constrain(null);
                        query.Descend(item.FieldName).Constrain(item.Value).SizeGt().And(q);
                    }
                }

                if (item.Operator == Operators.Smaller)
                {
                    if (i > 0)
                    {
                        query.Descend(item.FieldName).Constrain(item.Value).SizeLe();
                    }
                    else
                    {
                        var q = query.Constrain(null);
                        query.Descend(item.FieldName).Constrain(item.Value).SizeLe().And(q);
                    }
                }

                if (item.Operator == Operators.SmallerThan)
                {
                    if (i > 0)
                    {
                        query.Descend(item.FieldName).Constrain(item.Value).SizeLt();
                    }
                    else
                    {
                        var q = query.Constrain(null);
                        query.Descend(item.FieldName).Constrain(item.Value).SizeLt().And(q);
                    }
                }

                i++;
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
