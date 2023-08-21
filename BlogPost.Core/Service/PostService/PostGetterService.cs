using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO;
using BlogPost.Core.ServiceContracts;

namespace BlogPost.Core.Service.PostService
{
    public class PostGetterService : IPostGetterService
    {
        private readonly IPostRepository _postRepository;

        public PostGetterService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<PostResponseDTO> GetPostByIdAsync(Guid postId)
        {
            Post? postFromGet = await _postRepository.GetPostByIdAsync(postId);
            if (postFromGet == null)
            {
                return null; } //todo:return custom exception

            PostResponseDTO PostResponse = postFromGet.ToPostResponse();

            return PostResponse;
        }
    }
}
