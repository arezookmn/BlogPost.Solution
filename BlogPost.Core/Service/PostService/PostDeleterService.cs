using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.ServiceContracts.PostServicesInterface;

namespace BlogPost.Core.Service.PostService
{
    public class PostDeleterService : IPostDeleterService
    {
        private readonly IPostRepository _postRepository;

        public PostDeleterService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task DeletePostAsync(Guid postId) //todo:on delete cascade,also any comment deleted
        {
            Post postFromGet = await _postRepository.GetPostByIdAsync(postId);

            if (postFromGet == null) throw new ArgumentException("postId is invalid !");

            await _postRepository.DeletePostAsync(postFromGet);
        }

        public async Task SoftDeletePostAsync(Guid postId)
        {
            Post postFromGet = await _postRepository.GetPostByIdAsync(postId);

            if (postFromGet == null) throw new ArgumentException("postId is invalid !");

            await _postRepository.SoftDeletePostAsync(postFromGet);
        }
    }
}
