using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Infrustructure.DbContext;

namespace BlogPost.Infrustructure.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        } 
        public async Task<Post> AddPostAsync(Post post)
        {
            post.PostID = Guid.NewGuid();
            post.DateCreated = DateTime.UtcNow;

            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }
    }
}
