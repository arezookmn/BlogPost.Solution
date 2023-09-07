using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlogPost.Core.Domain.Entities.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public Guid? AuthorId { get; set; }
        public Author Author { get; set; }

        public ICollection<Comment>? Comments { get; } = new List<Comment>();
        public ICollection<UserLike>? LikedArticles { get; } = new List<UserLike>();


    }
}
