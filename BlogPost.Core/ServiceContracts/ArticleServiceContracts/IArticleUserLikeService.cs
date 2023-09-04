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
        Task<List<UserLikeResponseDTO>> GetUserLikeOfSpecificArticle(Guid articleId);
        Task<int> GetCountOfUserLikeOfSpecificArticle(Guid articleId);
        Task<bool> IsUserLikedArticle(Guid userId, Guid articleId);
        Task<bool> DeleteUserLike(Guid userId, Guid articleId);

    }
}
