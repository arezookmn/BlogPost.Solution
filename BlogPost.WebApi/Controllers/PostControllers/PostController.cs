using BlogPost.Core.Domain.Entities;
using BlogPost.Core.DTO;
using BlogPost.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.WebApi.Controllers.PostControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private  readonly IPostAdderService _postAdderService;
        private readonly IPostUpdaterService _postUpdaterService;   
        public PostController(IPostAdderService postAdderService, IPostUpdaterService postUpdaterService)
        {
            _postAdderService = postAdderService;
            _postUpdaterService = postUpdaterService;   
        }
        [HttpPost]
        public async Task<ActionResult<PostResponseDTO>> PostBlogPost(CreatePostRequestDTO postRequestDto)
        {
            PostResponseDTO postResponse_FromService = await  _postAdderService.CreatePostAsync(postRequestDto);

            return postResponse_FromService;
        }


        [HttpPut]
        public async Task<ActionResult<PostResponseDTO>> UpdatePost(UpdatePostRequestDTO postRequestDto)
        {
            PostResponseDTO postResponse_FromService = await _postUpdaterService.UpdatePost(postRequestDto);

            return postResponse_FromService;
        }
    }
}
