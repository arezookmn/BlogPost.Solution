using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.ServiceContracts.PostServicesInterface;

namespace BlogPost.Core.Service.PostService
{
    public class ArticleDeleterService : IArticleDeleterService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleDeleterService(IArticleRepository articleRepository)
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
    }
}
