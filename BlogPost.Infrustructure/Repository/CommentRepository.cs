using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Infrustructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Infrustructure.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentRepository(ApplicationDbContext dbContext)
        {
                _dbContext = dbContext;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            _dbContext.Comments.Add(comment);   
            await _dbContext.SaveChangesAsync();    
            return comment; 
        }

        public async Task DeleteComment(Comment comment)
        {
            _dbContext.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllCommentsOfSpecificPost(Guid postId)
        {
            return await _dbContext.Comments.Where(p => p.PostID == postId).ToListAsync();
        }

        public async Task<Comment> GetCommentById(Guid commentId)
        {
            return await _dbContext.Comments.FirstOrDefaultAsync(c => c.CommentID == commentId);
        }
    }
}
