using BlogPost.Core.Domain.Entities.IdentityEntities;
using BlogPost.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.DTO.ArticleDTO
{
    public class UserLikeResponseDTO
    {
        public Guid UserLikeId { get; set; }
        public Guid UserId { get; set; }
        public Guid ArticleId { get; set; }
    }

    public static class UserLikeExtention
    {
        public static UserLikeResponseDTO ToUserLikeResponse(this UserLike userLike)
        {
            return new UserLikeResponseDTO
            {
                UserLikeId = userLike.UserLikeId,
                UserId = userLike.UserId,
                ArticleId = userLike.ArticleId
            };
        }
    }
}
