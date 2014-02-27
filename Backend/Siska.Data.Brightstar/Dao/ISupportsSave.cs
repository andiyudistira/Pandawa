namespace Siska.Data.Dao
{

    /// <summary>
    /// Role interface for DAOs that support saves and updates to entities.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TId">Entity id type.</typeparam>
    public interface ISupportsSave<TEntity, TId>
    {
        /// <summary>
        /// Save any changes on context.
        /// </summary>
        void SaveChanges();
    }
}
