using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Siska.Core;

namespace Siska.Data.Dao
{
    /// <summary>
    /// Generic DAO for general retrieval and operation method
    /// </summary>
    /// <typeparam name="TEntity">Entity to operate with</typeparam>
    /// <typeparam name="TId">Entity ID type</typeparam>
    public interface IDao<TEntity, TId>
    {
        void TestPrepare();

        /// <summary>
        /// Finds entity with given id.
        /// </summary>
        /// <param name="id">The id to search with.</param>
        /// <returns>Found entity or null if not found.</returns>
        TEntity Get(TId id);

        /// <summary>
        /// Returns all entities of given type.
        /// The result may be different than in database based on
        /// filters or other locked search criteria for search.
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetAll();

        /// <summary>
        /// Return entities matches a string criteria.
        /// </summary>
        /// <param name="criteria">criteria for selection in string</param>
        /// <returns>Matching entities</returns>
        IList<TEntity> GetByCriteria(List<CriteriaParam> Param);

        /// <summary>
        /// Return entities matches a string criteria with paging.
        /// </summary>
        /// <param name="criteria">criteria for selection in string</param>
        /// <returns>Matching entities</returns>
        IList<TEntity> GetByCriteriaWithPaging(int page, int maxRow, out int numberOfPages, List<CriteriaParam> Param);
    }
}
