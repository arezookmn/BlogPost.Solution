using BlogPost.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.DTO
{
    public class UpdatePostRequestDTO
    {
        public Guid PostID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(300, ErrorMessage = "Title cannot exceed 300 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Main content is required.")]
        public string MainContent { get; set; }

        [Url(ErrorMessage = "Invalid image URL format.")]
        public string? ImageUrl { get; set; }


        public Post ToPost()
        {
            return new Post
            {
                Title = Title,
                MainContent = MainContent,
                ImageUrl = ImageUrl,
            };
        }
    }
}
