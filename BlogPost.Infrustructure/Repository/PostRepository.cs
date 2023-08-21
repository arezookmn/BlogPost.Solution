using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Infrustructure.DbContext;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            Post? post = await _dbContext.Posts.FirstOrDefaultAsync(t => t.PostID == postId);
            return post;
        }

        public async Task<Post> UpdatePostAsync(Post post)
        {
            var existingPost = await _dbContext.Posts.FirstOrDefaultAsync(t => t.PostID == post.PostID);

            if (existingPost == null) return null;

            existingPost.ImageUrl = post.ImageUrl;  
            existingPost.MainContent = post.MainContent;    
            existingPost.Title = post.Title;    
            existingPost.DateUpdated = DateTime.UtcNow;

            _dbContext.Entry(existingPost).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return post;
        }


        public async Task DeletePostAsync(Post post)
        {
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SoftDeletePostAsync(Post post)
        {
            post.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
