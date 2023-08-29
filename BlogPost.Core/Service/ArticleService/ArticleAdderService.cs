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
    public class ArticleAdderService : IArticleAdderService
    {
        private readonly IArticleRepository _postRepository;

        public ArticleAdderService(IArticleRepository postRepository)
        {
            _postRepository = postRepository;
        }   
        public async Task<ArticleResponseDTO> CreateArticleAsync(CreateArticleRequestDTO requestDto)
        {
            if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));

            ValidationHelper.ModelValidation(requestDto);

            //Business validation //todo:adding business validation for adding post
            Article article_FromRequest = requestDto.ToArticle();

            article_FromRequest.ArticleID = Guid.NewGuid();
            article_FromRequest.DateCreated = DateTime.UtcNow;

            Article article_FromRepository = await _postRepository.AddArticleAsync(article_FromRequest);

            ArticleResponseDTO articleResponseDTO = article_FromRepository.ToArticleResponse();

            return articleResponseDTO;
        }
    }
}
