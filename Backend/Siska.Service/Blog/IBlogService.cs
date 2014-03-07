namespace Siska.Service.Blog
{
    using Siska.Core;
    using Siska.Data.Model.Blog;
    using System.Collections.Generic;

    public interface IBlogService
    {
        ServiceResponse LatestPost();

        int TotalPost();
    }
}
