namespace Siska.Data.Model.Blog
{
    using System;
    using System.Collections.Generic;
    using BrightstarDB.EntityFramework;

    [Entity]
    public interface ITag
    {
        [Identifier]
        string Id { get; }
        string Name { get; set; }
        string UrlSlug { get; set; }
        string Description { get; set; }

        ICollection<IPost> Posts { get; set; }
    }
}
