using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.Domain.RepositoryContracts
{
    public interface IPostRepository
    {
        Task<Post> AddPostAsync(Post post);

        Task<Post> UpdatePostAsync(Post post);

        Task<Post> GetPostByIdAsync(Guid postId);

        Task DeletePostAsync(Post post);
        Task SoftDeletePostAsync(Post Post);
    }
}
