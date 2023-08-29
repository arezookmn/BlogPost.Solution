using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.Domain.RepositoryContracts
{
    public interface ICategoryRepository
    {
        Task<List<Article>> GetArticlesOfCategory(int categoryId);
        Task<Category> GetCategoryById(int categoryId);

        Task<Category> AddCategory(Category category);
        // Task<List<Post>> GetNewestPostsOfCategoryByPagination(int categoryId);

    }
}
