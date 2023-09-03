using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.ArticleDTO;
using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.ServiceContracts.ArticleServiceContracts;
using Services.Helper;

namespace BlogPost.Core.Service.ArticleService
{
    public class ArticleUserLikeService : IArticleUserLikeService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleService _articleService;
        public ArticleUserLikeService(IArticleRepository articleRepository, IArticleService articleService)
        {
            _articleRepository = articleRepository;
            _articleService = articleService;
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

        public async Task<int> GetCountOfUserLikeOfSpecificArticle(Guid articleId)
        {
            List<UserLikeResponseDTO> userLikes = await GetUserLikeOfSpecificArticle(articleId);
            int userLikeCount = userLikes.Count();
            return userLikeCount;
        }

        public async Task<List<UserLikeResponseDTO>> GetUserLikeOfSpecificArticle(Guid articleId)
        {
            //ArticleResponseDTO articleResponse = await _articleService.GetArticleByIdAsync(articleId);
            //if (articleResponse.ArticleID == articleId) throw new ArgumentException("Id is not valid!");

            List<UserLike> userLikes = await _articleRepository.GetUserLikeOfArticle(articleId);

            List<UserLikeResponseDTO> userLikeResponses = userLikes.Select(u => u.ToUserLikeResponse()).ToList();
            return userLikeResponses;
        }

        public async Task<bool> IsUserLikedArticle(Guid userId, Guid articleId)
        {
           return await _articleRepository.IsUserLikedArticle(userId, articleId);
        }
    }
}
