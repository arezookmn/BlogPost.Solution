using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.ServiceContracts.PostServicesInterface;
using Services.Helper;

namespace BlogPost.Core.Service.PostService
{
    public class ArticleUpdaterService : IArticleUpdaterService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleGetterService _articleGetterService; 

        public ArticleUpdaterService(IArticleRepository articleRepository, IArticleGetterService articleGetterService)
        {
            _articleRepository = articleRepository;
            _articleGetterService = articleGetterService;
        }
        public async Task<ArticleResponseDTO> UpdateArticle(UpdateArticleRequestDTO updateRequestDto)
        {
            if (updateRequestDto == null) throw new ArgumentNullException(nameof(updateRequestDto));

            ValidationHelper.ModelValidation(updateRequestDto);
            //todo: add business validation : for example checking is tha post is not spam 

            ArticleResponseDTO articleResponseFromGet =await _articleGetterService.GetArticleByIdAsync(updateRequestDto.ArticleID);
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
            
            Article updatedArticle =  await _articleRepository.UpdateArticleAsync(articleToBeUpdated);

            ArticleResponseDTO articleResponse = updatedArticle.ToArticleResponse();

            return articleResponse;
        }
    }
}
