using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.Service.CategoryServices;
using BlogPost.Core.ServiceContracts.CategoryServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlogPost.WebApi.Controllers.CategoryController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryItemGetterServiceInterface _categoryitemGetterService;

        public CategoryController(ICategoryItemGetterServiceInterface categoryItemGetterService)
        {
            _categoryitemGetterService = categoryItemGetterService;
        }

        [HttpGet]
        //todo: adding filter and pagination - filter by date name author
        public async Task<ActionResult<List<PostResponseDTO>>> GetPostsOfCategory(int categoryId)
        {
            return await _categoryitemGetterService
                .GetPostsOfCategory(categoryId);
        }
    }
}
