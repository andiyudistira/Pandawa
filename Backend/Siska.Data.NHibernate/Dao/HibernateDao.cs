using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Siska.Core;
using System.Collections;
using System.Transactions;
using NHibernate.Context;

namespace Siska.Data.NHibernate.Dao
{
    /// <summary>
    /// Base class for data access operations.
    /// </summary>
    public abstract class HibernateDao
    {
        protected readonly Func<ISession> getSession;

        public HibernateDao(Func<ISession> getSession)
		{
			this.getSession = getSession;
		}

        protected IList<T> GetAll<T>() where T : class
        {
            IList<T> result = null;

            using (var s = getSession())
            {
                ICriteria criteria = getSession().CreateCriteria<T>();
                result = criteria.List<T>();
            }

            return result;
        }

        protected IList<T> CreateCriteria<T>(List<CriteriaParam> itemParams) where T : class
        {
            IList<T> result = null;

            using (var s = getSession())
            {
                ICriteria criteria = s.CreateCriteria<T>();

                foreach (CriteriaParam item in itemParams)
                {
                    if ((!item.Operator.Equals(Operators.And)) && (!item.Operator.Equals(Operators.Or)))
                    {
                        criteria.Add(CreateCriterion(item));
                    }
                    else
                    {
                        if (item.Operator.Equals(Operators.Or))
                        {
                            ICriterion leftCriterion = CreateCriterion(item.Left);
                            ICriterion rightCriterion = CreateCriterion(item.Right);
                            criteria.Add(Restrictions.Or(leftCriterion, rightCriterion));
                        }
                        else if (item.Operator.Equals(Operators.And))
                        {
                            ICriterion leftCriterion = CreateCriterion(item.Left);
                            ICriterion rightCriterion = CreateCriterion(item.Right);
                            criteria.Add(Restrictions.And(leftCriterion, rightCriterion));
                        }
                    }
                }

                result = criteria.List<T>();
            }

            return result;
        }

        protected ICriteria CreateCriteriaOnly<T>(List<CriteriaParam> itemParams) where T : class
        {
            using (var s = getSession())
            {
                ICriteria criteria = s.CreateCriteria<T>();

                foreach (CriteriaParam item in itemParams)
                {
                    if ((!item.Operator.Equals(Operators.And)) && (!item.Operator.Equals(Operators.Or)))
                    {
                        criteria.Add(CreateCriterion(item));
                    }
                    else
                    {
                        if (item.Operator.Equals(Operators.Or))
                        {
                            ICriterion leftCriterion = CreateCriterion(item.Left);
                            ICriterion rightCriterion = CreateCriterion(item.Right);
                            criteria.Add(Restrictions.Or(leftCriterion, rightCriterion));
                        }
                        else if (item.Operator.Equals(Operators.And))
                        {
                            ICriterion leftCriterion = CreateCriterion(item.Left);
                            ICriterion rightCriterion = CreateCriterion(item.Right);
                            criteria.Add(Restrictions.And(leftCriterion, rightCriterion));
                        }
                    }
                }

                return criteria;
            }
        }

        protected ICriterion CreateCriterion(CriteriaParam item)
        {
           ICriterion criterion = null;

           if (item.Operator.Equals(Operators.Equals))
           {
              criterion = Restrictions.Eq(item.FieldName, item.Value);
           }
           else if (item.Operator.Equals(Operators.NotEquals))
           {
              criterion = Restrictions.Not(Restrictions.Eq(item.FieldName, item.Value));
           }
           else if (item.Operator.Equals(Operators.Greater))
           {
              criterion = Restrictions.Ge(item.FieldName, item.Value);
           }
           else if (item.Operator.Equals(Operators.GreaterThan))
           {
              criterion = Restrictions.Gt(item.FieldName, item.Value);
           }
           else if (item.Operator.Equals(Operators.Smaller))
           {
              criterion = Restrictions.Le(item.FieldName, item.Value);
           }
           else if (item.Operator.Equals(Operators.SmallerThan))
           {
              criterion = Restrictions.Lt(item.FieldName, item.Value);
           }
           else if (item.Operator.Equals(Operators.In))
           {
              criterion = Restrictions.In(item.FieldName, item.Value as ICollection);
           }
           else if (item.Operator.Equals(Operators.NotIn))
           {
              criterion = Restrictions.Not(Restrictions.In(item.FieldName, item.Value as ICollection));
           }
           else if (item.Operator.Equals(Operators.Between))
           {
              criterion = Restrictions.Between(item.FieldName, item.ValueLo, item.ValueHigh);
           }

           return criterion;
        }
    }
}
