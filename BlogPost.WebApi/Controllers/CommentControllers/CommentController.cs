using BlogPost.Core.DTO.CommentDTO;
using BlogPost.Core.ServiceContracts.CommentServicesInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.WebApi.Controllers.CommentControllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<ActionResult<CommentResponseDTO>> PostComment(CreateCommentRequestDTO commentRequestDto)
        {
            return await  _commentService.CreateComment(commentRequestDto);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteComment(Guid commentId)
        {
            return await _commentService.DeleteComment(commentId);
        }

        [HttpGet("{articleId}")]
        public async Task<ActionResult<List<CommentResponseDTO>>> GetAllComments(Guid articleId)
        {
           return await _commentService.GetAllCommentsOfSpecificArticle(articleId);
        }


        [HttpPut("Edit/{commentId}")]
        public async Task<ActionResult<CommentResponseDTO>> UpdateComment(Guid commentId,UpdateCommentRequestDTO commentRequest)
        {
            if (commentId != commentRequest.CommentID)
            {
                return BadRequest();
            }

            return await _commentService.UpdateComment(commentId, commentRequest);
        }
    }
}
