using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.ServiceContracts.PostServicesInterface;

namespace BlogPost.Core.Service.PostService
{
    public class ArticleGetterService : IArticleGetterService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleGetterService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<ArticleResponseDTO> GetArticleByIdAsync(Guid postId)
        {
            Article? articleFromGet = await _articleRepository.GetArticleByIdAsync(postId);
            if (articleFromGet == null)
            {
                return null; } //todo:return custom exception

            ArticleResponseDTO articleResponse = articleFromGet.ToArticleResponse();

            return articleResponse;
        }
    }
}
