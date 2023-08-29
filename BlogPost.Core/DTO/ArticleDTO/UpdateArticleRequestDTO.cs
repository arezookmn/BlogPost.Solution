using BlogPost.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.DTO.PostDTO
{
    public class UpdateArticleRequestDTO
    {
        public Guid ArticleID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(300, ErrorMessage = "Title cannot exceed 300 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Main content is required.")]
        public string MainContent { get; set; }

        [Url(ErrorMessage = "Invalid image URL format.")]
        public string? ImageUrl { get; set; }


        public Article ToPost()
        {
            return new Article
            {
                Title = Title,
                MainContent = MainContent,
                ImageUrl = ImageUrl,
            };
        }
    }
}
