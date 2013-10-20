using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siska.Data.BModel.Pos;
using Siska.Data.Dao;

namespace Siska.Data.BDao
{
    public interface IUserDao : IDao<IUser, string>, ISupportsSave<IUser, string>,
             ISupportsPaging<IUser>, ISupportsDelete<IUser>
    {
        IUser CreateNew();
    }
}
