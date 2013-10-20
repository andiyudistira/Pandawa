using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siska.Data.Dao;
using Siska.Data.BModel.Pos;

namespace Siska.Data.BDao
{
    public interface IUserSessionDao : IDao<IUserSession, string>, ISupportsSave<IUserSession, string>,
             ISupportsPaging<IUserSession>, ISupportsDelete<IUserSession>
    {
    }
}
