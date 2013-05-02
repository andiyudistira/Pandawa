using Siska.Data.Model.Acc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Siska.Data.Common.Acc
{
    public interface IAccountDao : IDao<AccAccount, int>, ISupportsSave<AccAccount, int>,
             ISupportsPaging<AccAccount>, ISupportsDelete<AccAccount>
    {
    }
}
