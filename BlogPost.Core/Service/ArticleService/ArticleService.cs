using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.ServiceContracts.ArticleServiceContracts;
using Services.Helper;

namespace BlogPost.Core.Service.ArticleService
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task DeleteArticleAsync(Guid articleId) //todo:on delete cascade,also any comment deleted
        {
            Article articleFromGet = await _articleRepository.GetArticleByIdAsync(articleId);

            if (articleFromGet == null) throw new ArgumentException("postId is invalid !");

            await _articleRepository.DeleteArticleAsync(articleFromGet);
        }

        public async Task SoftDeleteArticleAsync(Guid articleId)
        {
            Article articleFromGet = await _articleRepository.GetArticleByIdAsync(articleId);

            if (articleFromGet == null) throw new ArgumentException("articleId is invalid !");

            await _articleRepository.SoftDeleteArticleAsync(articleFromGet);
        }

        public async Task<ArticleResponseDTO> CreateArticleAsync(CreateArticleRequestDTO requestDto)
        {
            if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));

            ValidationHelper.ModelValidation(requestDto);

            //Business validation //todo:adding business validation for adding post
            Article article_FromRequest = requestDto.ToArticle();

            article_FromRequest.ArticleID = Guid.NewGuid();
            article_FromRequest.DateCreated = DateTime.UtcNow;

            Article article_FromRepository = await _articleRepository.AddArticleAsync(article_FromRequest);

            ArticleResponseDTO articleResponseDTO = article_FromRepository.ToArticleResponse();

            return articleResponseDTO;
        }

        public async Task<ArticleResponseDTO> GetArticleByIdAsync(Guid postId)
        {
            Article? articleFromGet = await _articleRepository.GetArticleByIdAsync(postId);
            if (articleFromGet == null)
            {
                return null;
            } //todo:return custom exception

            ArticleResponseDTO articleResponse = articleFromGet.ToArticleResponse();

            return articleResponse;
        }

        public async Task<ArticleResponseDTO> UpdateArticle(UpdateArticleRequestDTO updateRequestDto)
        {
            if (updateRequestDto == null) throw new ArgumentNullException(nameof(updateRequestDto));

            ValidationHelper.ModelValidation(updateRequestDto);
            //todo: add business validation : for example checking is tha post is not spam 

            ArticleResponseDTO articleResponseFromGet = await GetArticleByIdAsync(updateRequestDto.ArticleID);
            //todo: null checking and returning custom exception

            Article articleToBeUpdated = new Article()
            {
                ArticleID = articleResponseFromGet.ArticleID,
                Title = updateRequestDto.Title,
                MainContent = updateRequestDto.MainContent,
                ImageUrl = updateRequestDto.ImageUrl,
                DateCreated = articleResponseFromGet.DateCreated,
                DateUpdated = articleResponseFromGet.DateUpdated
            };

            Article updatedArticle = await _articleRepository.UpdateArticleAsync(articleToBeUpdated);

            ArticleResponseDTO articleResponse = updatedArticle.ToArticleResponse();

            return articleResponse;
        }
    }
}
