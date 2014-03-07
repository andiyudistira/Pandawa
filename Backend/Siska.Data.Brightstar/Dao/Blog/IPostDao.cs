namespace Siska.Data.Dao.Blog
{
    using Siska.Data.Model.Blog;
    using BrightstarDB.EntityFramework;

    public interface IPostDao : IDao<IPost, string>, ISupportsSave<IPost, string>,
             ISupportsPaging<IPost>, ISupportsDelete<IPost>
    {
    }
}
