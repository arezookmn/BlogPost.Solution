using BlogPost.Core.Domain.Entities;
using BlogPost.Core.DTO;
using BlogPost.Core.ServiceContracts.PostServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.WebApi.Controllers.PostControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePostController : ControllerBase
    {
        private  readonly IPostAdderService _postAdderService;
        public CreatePostController(IPostAdderService postAdderService)
        {
            _postAdderService = postAdderService;
        }
        [HttpPost]
        public async Task<ActionResult<PostResponseDTO>> PostBlogPost(CreatePostRequestDTO postRequestDto)
        {
            PostResponseDTO postResponse_FromService = await  _postAdderService.CreatePostAsync(postRequestDto);

            return postResponse_FromService;
        }
    }
}
