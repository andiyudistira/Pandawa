using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siska.Data.BModel.Pos;
using Siska.Data.Dao;

namespace Siska.Data.BDao
{
    public interface IRoleDao : IDao<IRole, string>, ISupportsSave<IRole, string>,
             ISupportsPaging<IRole>, ISupportsDelete<IRole>
    {
        IRole CreateNew();
    }
}
