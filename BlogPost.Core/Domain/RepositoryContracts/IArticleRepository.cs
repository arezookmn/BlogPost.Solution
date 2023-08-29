using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.Domain.RepositoryContracts
{
    public interface IArticleRepository
    {
        Task<Article> AddArticleAsync(Article article);

        Task<Article> UpdateArticleAsync(Article article);

        Task<Article> GetArticleByIdAsync(Guid articleId);

        Task DeleteArticleAsync(Article article);
        Task SoftDeleteArticleAsync(Article article);
    }
}
