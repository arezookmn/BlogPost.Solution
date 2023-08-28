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

        [HttpGet("{postId}")]
        public async Task<ActionResult<List<CommentResponseDTO>>> GetAllComments(Guid postId)
        {
           return await _commentService.GetAllCommentsOfSpecificPost(postId);
        }
    }
}
