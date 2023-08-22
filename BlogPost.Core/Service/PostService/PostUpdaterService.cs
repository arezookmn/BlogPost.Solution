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
    public class PostUpdaterService : IPostUpdaterService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostGetterService _postGetterService; 

        public PostUpdaterService(IPostRepository postRepository, IPostGetterService postGetterService)
        {
            _postRepository = postRepository;
            _postGetterService = postGetterService;
        }
        public async Task<PostResponseDTO> UpdatePost(UpdatePostRequestDTO updateRequestDto)
        {
            if (updateRequestDto == null) throw new ArgumentNullException(nameof(updateRequestDto));

            ValidationHelper.ModelValidation(updateRequestDto);
            //todo: add business validation : for example checking is tha post is not spam 

            PostResponseDTO postResponseFromGet =await _postGetterService.GetPostByIdAsync(updateRequestDto.PostID);
            //todo: null checking and returning custom exception

            Post postToBeUpdated = new Post()
            {
                PostID = postResponseFromGet.PostID,
                Title = updateRequestDto.Title,
                MainContent = updateRequestDto.MainContent,
                ImageUrl = updateRequestDto.ImageUrl,
                DateCreated = postResponseFromGet.DateCreated,
                DateUpdated = postResponseFromGet.DateUpdated
            };
            
            Post updatedPost =  await _postRepository.UpdatePostAsync(postToBeUpdated);

            PostResponseDTO postResponse = updatedPost.ToPostResponse();

            return postResponse;
        }
    }
}
