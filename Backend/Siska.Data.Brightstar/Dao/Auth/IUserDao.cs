namespace Siska.Data.Dao.Auth
{
    using BrightstarDB.EntityFramework;
    using Siska.Data.Model.Auth;

    public interface IUserDao : IDao<IUser, string>, ISupportsSave<IUser, string>,
             ISupportsPaging<IUser>, ISupportsDelete<IUser>
    {
        IEntitySet<IUser> Users { get; }

        IUser CreateNew();
    }
}
