using BlogPost.Core.Domain.Entities;
using BlogPost.Core.DTO.PostDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using BlogPost.Core.DTO.ArticleDTO;
using BlogPost.Core.ServiceContracts.ArticleServiceContracts;
using BlogPost.Core.ServiceContracts.IdentityServiceContracts;

namespace BlogPost.WebApi.Controllers.PostControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IArticleUserLikeService _articleUserLikeService;
        private readonly ICurrentUserDetails _currentUserDetails;
        public ArticleController(IArticleService articleService, IArticleUserLikeService userLikeService, ICurrentUserDetails currentUserDetails)
        {
            _articleService = articleService;
            _articleUserLikeService = userLikeService;
            _currentUserDetails = currentUserDetails;
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public async Task<ActionResult<ArticleResponseDTO>> PostArticle(CreateArticleRequestDTO articleRequestDto)
        {
            ArticleResponseDTO articleResponse_FromService = await  _articleService.CreateArticleAsync(articleRequestDto);

            return articleResponse_FromService;
        }


        [HttpPut]
        [Authorize(Roles = "Author")]
        public async Task<ActionResult<ArticleResponseDTO>> UpdateArticle(UpdateArticleRequestDTO articleRequestDto)
        {
            ArticleResponseDTO articleResponse_FromService = await _articleService.UpdateArticle(articleRequestDto);

            return articleResponse_FromService;
        }

        [HttpDelete]
        [Authorize(Roles = "Author")]
        public async Task<ActionResult<bool>> DeleteArticle(Guid articleId)
        {
           await _articleService.DeleteArticleAsync(articleId);

           ArticleResponseDTO? articleFromGet = await _articleService.GetArticleByIdAsync(articleId);

           if (articleFromGet is  null) return true;

           else return false;
        }

        [HttpPost("like/{articleId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<UserLikeResponseDTO>> PostLikeArticle(Guid articleId)
        {
            Guid userId = await _currentUserDetails.GetCurrentUserId();
            if (await _articleUserLikeService.IsUserLikedArticle(userId,articleId))
            {
                return Ok(); //todo:what did i do in that condition ,maybe nothing !
            }
                CreateUserLikeDTO userLike = new CreateUserLikeDTO()
            {
                UserId = userId,
                ArticleId = articleId
            };
            UserLikeResponseDTO userLikeResponse = await _articleUserLikeService.CreateUserLike(userLike);

            return Ok(userLikeResponse);
        }

        [HttpGet("likeCount/{articleId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<int>> GetCountOfLikeArticle(Guid articleId)
        {
            int userLikeCount = await _articleUserLikeService.GetCountOfUserLikeOfSpecificArticle(articleId);

            return Ok(userLikeCount);
        }


        [HttpDelete("like/{articleId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<bool>> UnlikeArticle(Guid articleId)
        {
            Guid userId = await _currentUserDetails.GetCurrentUserId();

            bool isUnliked = await _articleUserLikeService.DeleteUserLike(userId, articleId);

            return isUnliked;
        }

    }

 
}
