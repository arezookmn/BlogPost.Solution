using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.DTO.CategoryDTO;

namespace BlogPost.Core.ServiceContracts.CategoryServiceInterface
{
    public interface ICategoryAdminService
    {
        Task<CategoryResponseDTO> AddCategory(CreateCategoryDTO createCategoryDto);
    }
}
