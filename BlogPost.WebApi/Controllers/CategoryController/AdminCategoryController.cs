using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.CategoryDTO;
using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.ServiceContracts.CategoryServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.WebApi.Controllers.CategoryController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCategoryController : ControllerBase
    {
        private readonly ICategoryAdminService _categoryAdminService;

        public AdminCategoryController(ICategoryAdminService categoryAdminService)
        {
            _categoryAdminService = categoryAdminService;
        }

        //for add delete update and remove category that allowed only by admin 
        [HttpPost]
        public async Task<ActionResult<CategoryResponseDTO>> PostCategory(CreateCategoryDTO categoryDTO)
        {
            return await _categoryAdminService.AddCategory(categoryDTO);
        } 
    }
}
