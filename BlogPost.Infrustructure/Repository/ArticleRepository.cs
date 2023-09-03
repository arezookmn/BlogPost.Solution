using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Infrustructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Infrustructure.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ArticleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        } 
        public async Task<Article> AddArticleAsync(Article article)
        {
            await _dbContext.Articles.AddAsync(article);
            await _dbContext.SaveChangesAsync();
            return article;
        }

        public async Task<Article> GetArticleByIdAsync(Guid articleId)
        {
            Article? article = await _dbContext.Articles.FirstOrDefaultAsync(t => t.ArticleID == articleId);
            return article;
        }

        public async Task<Article> UpdateArticleAsync(Article article)
        {
            var existingArticle = await _dbContext.Articles.FirstOrDefaultAsync(t => t.ArticleID == article.ArticleID);

            if (existingArticle == null) return null;

            existingArticle.ImageUrl = article.ImageUrl;  
            existingArticle.MainContent = article.MainContent;    
            existingArticle.Title = article.Title;    
            existingArticle.DateUpdated = DateTime.UtcNow;
            existingArticle.ShortDescription = article.ShortDescription;
            existingArticle.TimeToRead = article.TimeToRead;

            _dbContext.Entry(existingArticle).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return article;
        }


        public async Task DeleteArticleAsync(Article article)
        {
            _dbContext.Articles.Remove(article);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SoftDeleteArticleAsync(Article article)
        {
            article.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserLike> AddUserLike(UserLike userLike)
        {
            _dbContext.UserLikes.Add(userLike);
            await _dbContext.SaveChangesAsync();
            return userLike;
        }
    }
}
