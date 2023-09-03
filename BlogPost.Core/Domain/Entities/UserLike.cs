using BlogPost.Core.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.Domain.Entities
{
    public class UserLike
    {
        public Guid UserLikeId { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid ArticleId { get; set; }
        public Article Article { get; set; }

        public DateTime LikeTime { get; set; }
    }
}
