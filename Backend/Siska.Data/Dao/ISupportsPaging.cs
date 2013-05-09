using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Siska.Data.Dao
{
    /// <summary>
    /// Role interface for DAOs that support retrieval and paging to entities.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ISupportsPaging<TEntity>
    {
        /// <summary>
        /// Return All entities with paging support.
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="maxRow">maximum row retrieved</param>
        /// <returns></returns>
        IList<TEntity> GetAll(int page, int maxRow, out int numberOfPages);
    }
}
