namespace Siska.Data.Dao.Blog
{
    using Siska.Data.Model.Blog;
    using BrightstarDB.EntityFramework;

    public interface ITagDao : IDao<ITag, string>, ISupportsSave<ITag, string>,
             ISupportsPaging<ITag>, ISupportsDelete<ITag>
    {
    }
}
