using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Infrustructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Infrustructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> AddCategory(Category category)
        { 
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryId);
        }

        public async Task<List<Article>> GetArticlesOfCategory(int categoryId)
        {
            return await _dbContext.Articles
                .Where(p => p.CategoryID == categoryId)
                .ToListAsync();
        }

        //todo: add another repository method that include pagination and newest and more liked posts
    }
}
