using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Siska.Data.Common
{
    /// <summary>
    /// Role interface for DAOs that support deletion of entities.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ISupportsDelete<TEntity>
    {
        /// <summary>
        /// Mark entity to be deleted within the repository.
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(TEntity entity);
    }
}
