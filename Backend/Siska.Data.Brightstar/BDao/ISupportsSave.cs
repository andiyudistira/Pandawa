namespace Siska.Data.BDao
{

    /// <summary>
    /// Role interface for DAOs that support saves and updates to entities.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TId">Entity id type.</typeparam>
    public interface ISupportsSave<TEntity, TId>
    {
        /// <summary>
        /// Add entity to the repository.
        /// </summary>
        /// <param name="entity">the entity to add</param>
        /// <returns>The added entity</returns>
        string Add(TEntity entity);

        /// <summary>
        /// Update entity in reporsitory.
        /// </summary>
        /// <param name="entity">The updated entity</param>
        void Update(TEntity entity);
    }
}
