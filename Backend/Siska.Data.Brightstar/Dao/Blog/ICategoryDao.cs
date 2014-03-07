namespace Siska.Data.Dao.Blog
{
    using Siska.Data.Model.Blog;
    using BrightstarDB.EntityFramework;

    public interface ICategoryDao : IDao<ICategory, string>, ISupportsSave<ICategory, string>,
             ISupportsPaging<ICategory>, ISupportsDelete<ICategory>
    {
    }
}
