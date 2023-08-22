using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.ServiceContracts.PostServicesInterface;
using Services.Helper;

namespace BlogPost.Core.Service.PostService
{
    public class PostAdderService : IPostAdderService
    {
        private readonly IPostRepository _postRepository;

        public PostAdderService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }   
        public async Task<PostResponseDTO> CreatePostAsync(CreatePostRequestDTO requestDto)
        {
            if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));

            ValidationHelper.ModelValidation(requestDto);

            //Business validation //todo:adding business validation for adding post
            Post post_FromRequest = requestDto.ToPost();

            post_FromRequest.PostID = Guid.NewGuid();
            post_FromRequest.DateCreated = DateTime.UtcNow;

            Post post_FromRepository = await _postRepository.AddPostAsync(post_FromRequest);

            PostResponseDTO postResponseDTO = post_FromRepository.ToPostResponse();

            return postResponseDTO;
        }
    }
}
