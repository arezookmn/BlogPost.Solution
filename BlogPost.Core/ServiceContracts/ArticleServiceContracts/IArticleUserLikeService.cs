using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.DTO.ArticleDTO;

namespace BlogPost.Core.ServiceContracts.ArticleServiceContracts
{
    public interface IArticleUserLikeService
    {
        Task<UserLikeResponseDTO> CreateUserLike(CreateUserLikeDTO createUserLikeDto);
    }
}
