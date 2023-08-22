using BlogPost.Core.Domain.Entities;
using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.ServiceContracts.PostServicesInterface;
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
        private readonly IPostDeleterService _postDeleterService;  
        private readonly IPostGetterService _postGetterService;
        public PostController(IPostAdderService postAdderService, IPostUpdaterService postUpdaterService, IPostDeleterService postDeleterService, IPostGetterService postGetterService)
        {
            _postAdderService = postAdderService;
            _postUpdaterService = postUpdaterService;   
            _postDeleterService = postDeleterService;
            _postGetterService = postGetterService; 
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

        [HttpDelete]
        public async Task<ActionResult<bool>> DeletePost(Guid postId)
        {
           await _postDeleterService.DeletePostAsync(postId);

           PostResponseDTO? postFromGet = await _postGetterService.GetPostByIdAsync(postId);

           if (postFromGet is  null) return true;

           else return false;
        }
    }
}
