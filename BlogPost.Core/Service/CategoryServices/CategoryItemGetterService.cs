using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.ServiceContracts.CategoryServiceInterface;

namespace BlogPost.Core.Service.CategoryServices
{
    public class CategoryItemGetterService : ICategoryItemGetterServiceInterface
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryItemGetterService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<PostResponseDTO>> GetPostsOfCategory(int categoryId)
        {
            Category category = await _categoryRepository.GetCategoryById(categoryId);

            List<Post> categoryPosts =await _categoryRepository.GetPostsOfCategory(categoryId);

            List<PostResponseDTO> categoryPostDtos = categoryPosts.Select(p => p.ToPostResponse()).ToList();

            return categoryPostDtos;
        }
    }
}
