namespace Siska.Data.Dao.Auth
{
    using Siska.Data.Model.Auth;
    using BrightstarDB.EntityFramework;

    public interface IUserSessionDao : IDao<IUserSession, string>, ISupportsSave<IUserSession, string>,
             ISupportsPaging<IUserSession>, ISupportsDelete<IUserSession>
    {
        IEntitySet<IUserSession> UserSessions { get; }

        IUserSession CreateNew();
    }
}
