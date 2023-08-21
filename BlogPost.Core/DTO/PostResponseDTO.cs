using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.DTO
{
    public class PostResponseDTO
    {
        public Guid PostID { get; set; }
        public string Title { get; set; }
        public string MainContent { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

    }

    public static class PostExtension
    {
        public static PostResponseDTO ToPostResponse(this Post post)
        {
            return new PostResponseDTO()
            {
                PostID = post.PostID,
                Title = post.Title,
                ImageUrl = post.ImageUrl,
                MainContent = post.MainContent,
                DateCreated = post.DateCreated,
                DateUpdated = post.DateUpdated, 
            };
        }
    }
}
