using BlogPost.Core.DTO.CommentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.Domain.RepositoryContracts
{
    public interface ICommentRepository
    {
        Task<Comment> CreateComment(Comment comment);

        Task DeleteComment(Comment comment);

        Task<Comment> GetCommentById(Guid commentId);

        Task<List<Comment>> GetAllCommentsOfSpecificArticle(Guid articleId);

        Task<Comment> EditComment(Comment comment);

    }
}
