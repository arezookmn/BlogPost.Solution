using BlogPost.Core.Domain.Entities;
using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.ServiceContracts.PostServicesInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlogPost.WebApi.Controllers.PostControllers
{
    [Authorize(Roles = "Author")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private  readonly IArticleAdderService _articleAdderService;
        private readonly IArticleUpdaterService _articleUpdaterService; 
        private readonly IArticleDeleterService _articleDeleterService;  
        private readonly IArticleGetterService _articleGetterService;
        public ArticleController(IArticleAdderService articleAdderService, IArticleUpdaterService articleUpdaterService, IArticleDeleterService articleDeleterService, IArticleGetterService articleGetterService)
        {
            _articleAdderService = articleAdderService;
            _articleDeleterService = articleDeleterService;   
            _articleGetterService = articleGetterService;
            _articleUpdaterService = articleUpdaterService; 
        }

        [HttpPost]
        public async Task<ActionResult<ArticleResponseDTO>> PostArticle(CreateArticleRequestDTO articleRequestDto)
        {
            ArticleResponseDTO articleResponse_FromService = await  _articleAdderService.CreateArticleAsync(articleRequestDto);

            return articleResponse_FromService;
        }


        [HttpPut]
        public async Task<ActionResult<ArticleResponseDTO>> UpdateArticle(UpdateArticleRequestDTO articleRequestDto)
        {
            ArticleResponseDTO articleResponse_FromService = await _articleUpdaterService.UpdateArticle(articleRequestDto);

            return articleResponse_FromService;
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteArticle(Guid articleId)
        {
           await _articleDeleterService.DeleteArticleAsync(articleId);

           ArticleResponseDTO? articleFromGet = await _articleGetterService.GetArticleByIdAsync(articleId);

           if (articleFromGet is  null) return true;

           else return false;
        }
    }
}
