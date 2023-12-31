﻿using System;
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
        Task<UserLike> AddUserLike(UserLike userLike);    
        Task<List<UserLike>> GetUserLikeOfArticle(Guid articleId);
        Task<bool> IsUserLikedArticle(Guid userId,Guid articleId);
        Task DeleteUserLike(UserLike userLike);
        Task<UserLike?> GetUserLike(Guid userId, Guid articleId);
    }
}
