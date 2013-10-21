namespace Siska.Data.BDao
{
    using Siska.Data.BModel.Pos;
    using BrightstarDB.EntityFramework;

    public interface IUserSessionDao : IDao<IUserSession, string>, ISupportsSave<IUserSession, string>,
             ISupportsPaging<IUserSession>, ISupportsDelete<IUserSession>
    {
        IEntitySet<IUserSession> UserSessions { get; }

        IUserSession CreateNew();
    }
}
