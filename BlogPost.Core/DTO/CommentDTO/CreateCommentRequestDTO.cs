using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.DTO.CommentDTO
{
    public class CreateCommentRequestDTO
    {
        [Required(ErrorMessage = "The PostID is required.")]
        public Guid PostID { get; set; }

        [Required(ErrorMessage = "The CommentText is required.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "The CommentText must be between {2} and {1} characters.")]
        public string CommentText { get; set; }

        [StringLength(100, ErrorMessage = "The NameOfCommentAuthor cannot exceed {1} characters.")]
        public string NameOfCommentAuthor { get; set; } = "Unknown";


        public Comment ToComment()
        {
            return new Comment()
            {
                PostID = PostID,
                CommentText = CommentText,
                NameOfCommentAuthor = NameOfCommentAuthor,
            };
        }

    }


}
