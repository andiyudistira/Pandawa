namespace Siska.Data.BDao
{
    using Siska.Data.BModel.Pos;
    using BrightstarDB.EntityFramework;

    public interface IRoleDao : IDao<IRole, string>, ISupportsSave<IRole, string>,
             ISupportsPaging<IRole>, ISupportsDelete<IRole>
    {
        IEntitySet<IRole> Roles { get; }

        IRole CreateNew();
    }
}
