using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siska.Data.Model.Pos;

namespace Siska.Data.Dao
{
    public interface IUserSessionDao : IDao<UserSession, Guid>, ISupportsSave<UserSession, Guid>,
             ISupportsPaging<UserSession>, ISupportsDelete<UserSession>
    {
    }
}
