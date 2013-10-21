namespace Siska.Data.BDao
{
    using BrightstarDB.EntityFramework;
    using Siska.Data.BModel.Pos;

    public interface IUserDao : IDao<IUser, string>, ISupportsSave<IUser, string>,
             ISupportsPaging<IUser>, ISupportsDelete<IUser>
    {
        IEntitySet<IUser> Users { get; }

        IUser CreateNew();
    }
}
