using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.CategoryDTO;
using BlogPost.Core.ServiceContracts.CategoryServiceInterface;
using Services.Helper;

namespace BlogPost.Core.Service.CategoryServices
{
    public class CategoryAdminService : ICategoryAdminService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryAdminService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponseDTO> AddCategory(CreateCategoryDTO createCategoryDto)
        {
            ValidationHelper.ModelValidation(createCategoryDto);

            //todo:business validation

            Category categoryResponse = await _categoryRepository.AddCategory(createCategoryDto.ToCategory());

            return new CategoryResponseDTO()
            {
                CategoryID = categoryResponse.CategoryID,
                CategoryName = categoryResponse.CategoryName
            };
        }

    }
}
