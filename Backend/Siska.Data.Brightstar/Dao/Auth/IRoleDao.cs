namespace Siska.Data.Dao.Auth
{
    using Siska.Data.Model.Auth;
    using BrightstarDB.EntityFramework;

    public interface IRoleDao : IDao<IRole, string>, ISupportsSave<IRole, string>,
             ISupportsPaging<IRole>, ISupportsDelete<IRole>
    {
        IEntitySet<IRole> Roles { get; }

        IRole CreateNew();
    }
}
