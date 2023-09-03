using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.ArticleDTO;
using BlogPost.Core.ServiceContracts.ArticleServiceContracts;
using Services.Helper;

namespace BlogPost.Core.Service.ArticleService
{
    public class ArticleUserLikeService : IArticleUserLikeService
    {
        private readonly IArticleRepository _articleRepository;
        public ArticleUserLikeService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<UserLikeResponseDTO> CreateUserLike(CreateUserLikeDTO createUserLikeDto)
        {
            ValidationHelper.ModelValidation(createUserLikeDto);

            UserLike userLike = createUserLikeDto.ToUserLike();
            userLike.UserLikeId = Guid.NewGuid();
            userLike.LikeTime = DateTime.UtcNow;
            UserLike userLikeAfterAdding = await _articleRepository.AddUserLike(userLike);

            UserLikeResponseDTO userLikeResponse = userLikeAfterAdding.ToUserLikeResponse();
            return userLikeResponse;
        }
    }
}
