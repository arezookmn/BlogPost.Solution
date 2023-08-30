using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.DTO.CommentDTO
{
    public class CommentResponseDTO
    {
        public Guid CommentID { get; set; }
        public Guid ArticleID { get; set; }
        public string CommentText { get; set; }
        public string? NameOfCommentAuthor { get; set; }
        public DateTime CreatedDate { get; set; }


    }

    public static class CommentExtension
    {
        public static CommentResponseDTO ToCommentResponseDto(this Comment comment)
        {
            return new CommentResponseDTO()
            {
                CommentID = comment.CommentID,
                ArticleID = comment.ArticleID,
                CommentText = comment.CommentText,
                NameOfCommentAuthor = comment.NameOfCommentAuthor,
                CreatedDate = comment.CreatedDate
            };
        }
    }
}
