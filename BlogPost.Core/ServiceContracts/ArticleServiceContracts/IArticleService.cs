using BlogPost.Core.DTO.PostDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.ServiceContracts.ArticleServiceContracts
{
    public interface IArticleService
    {
        Task<ArticleResponseDTO> CreateArticleAsync(CreateArticleRequestDTO requestDto);
        Task SoftDeleteArticleAsync(Guid articleId);
        Task DeleteArticleAsync(Guid articleId);
        Task<ArticleResponseDTO> GetArticleByIdAsync(Guid articleId);
        Task<ArticleResponseDTO> UpdateArticle(UpdateArticleRequestDTO requestDto);

    }
}
