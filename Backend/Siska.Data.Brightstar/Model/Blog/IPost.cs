namespace Siska.Data.Model.Blog
{
    using System;
    using System.Collections.Generic;
    using BrightstarDB.EntityFramework;

    [Entity]
    public interface IPost
    {         
        [Identifier]
        string Id { get; }
        string Title { get; set; }
        string ShortDescription { get; set; }
        string Description { get; set; }
        string Meta { get; set; }
        string UrlSlug { get; set; }
        bool Published { get; set; }
        DateTime PostedOn { get; set; }
        DateTime? Modified { get; set; }
        ICategory Category { get; set; }

        [InverseProperty("Posts")]
        ICollection<ITag> Tags { get; set; }
    }
}
