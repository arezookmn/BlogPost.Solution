using BlogPost.Core.Domain.Entities;
using BlogPost.Core.DTO.PostDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using BlogPost.Core.DTO.ArticleDTO;
using BlogPost.Core.ServiceContracts.ArticleServiceContracts;

namespace BlogPost.WebApi.Controllers.PostControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IArticleUserLikeService _articleUserLikeService;
        public ArticleController(IArticleService articleService, IArticleUserLikeService userLikeService)
        {
            _articleService = articleService;
            _articleUserLikeService = userLikeService;
        }

        [HttpPost]
        public async Task<ActionResult<ArticleResponseDTO>> PostArticle(CreateArticleRequestDTO articleRequestDto)
        {
            ArticleResponseDTO articleResponse_FromService = await  _articleService.CreateArticleAsync(articleRequestDto);

            return articleResponse_FromService;
        }


        [HttpPut]
        public async Task<ActionResult<ArticleResponseDTO>> UpdateArticle(UpdateArticleRequestDTO articleRequestDto)
        {
            ArticleResponseDTO articleResponse_FromService = await _articleService.UpdateArticle(articleRequestDto);

            return articleResponse_FromService;
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteArticle(Guid articleId)
        {
           await _articleService.DeleteArticleAsync(articleId);

           ArticleResponseDTO? articleFromGet = await _articleService.GetArticleByIdAsync(articleId);

           if (articleFromGet is  null) return true;

           else return false;
        }

        [HttpPost("like/{articleId}")]
        public async Task<ActionResult<UserLikeResponseDTO>> PostLikeArticle(Guid articleId)
        {
            ArticleResponseDTO articleResponse = await _articleService.GetArticleByIdAsync(articleId);
            CreateUserLikeDTO userLike = new CreateUserLikeDTO()
            {
                UserId = articleResponse.ApplicationUserId,
                ArticleId = articleId
            };
            UserLikeResponseDTO userLikeResponse = await _articleUserLikeService.CreateUserLike(userLike);

            return Ok(userLikeResponse);
        } 
    }
}
