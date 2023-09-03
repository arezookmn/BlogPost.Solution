using BlogPost.Core.Domain.Entities.IdentityEntities;
using BlogPost.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.DTO.ArticleDTO
{
    public class CreateUserLikeDTO
    {
        [Required(ErrorMessage = "User id is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Article id is required")]
        public Guid ArticleId { get; set; }

        public UserLike ToUserLike()
        {
            return new UserLike
            {
                UserId = UserId,
                ArticleId = ArticleId
            };
        }
    }
}
