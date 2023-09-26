using BlogPost.Core.Domain.Entities.IdentityEntities;
using BlogPost.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.Domain.Entities
{
    public class Author
    {

        public Guid AuthorId { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string ShortAbout { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<Article>? Articles { get; } = new List<Article>();
    }
}
