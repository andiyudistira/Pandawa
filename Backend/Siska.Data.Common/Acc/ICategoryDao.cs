using Siska.Data.Model.Acc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Siska.Data.Common.Acc
{
    public interface ICategoryDao : IDao<AccCategory, int>, ISupportsSave<AccCategory, int>,
             ISupportsPaging<AccCategory>, ISupportsDelete<AccCategory>
    {
    }
}
