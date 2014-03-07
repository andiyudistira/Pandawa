namespace Siska.Data.Model.Blog
{
    using System;
    using System.Collections.Generic;
    using BrightstarDB.EntityFramework;

    [Entity]
    public interface ICategory
    {
        [Identifier]
        string Id { get; }
        string Name { get; set; }
        string UrlSlug { get; set; }
        string Description { get; set; }

        [InverseProperty("Category")]
        ICollection<IPost> Posts { get; set; }
    }
}
