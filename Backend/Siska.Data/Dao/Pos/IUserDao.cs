﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siska.Data.Model.Pos;

namespace Siska.Data.Dao
{
    public interface IUserDao : IDao<User, int>, ISupportsSave<User, int>,
             ISupportsPaging<User>, ISupportsDelete<User>
    {
    }
}
