using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.DTO.PostDTO
{
    public class ArticleResponseDTO
    {
        public Guid ArticleID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string MainContent { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool CommentAllowing { get; set; }
        public int CategoryID { get; set; }
        public int LikeCount { get; set; }

        public int TimeToRead { get; set; }

    }

    public static class PostExtension
    {
        public static ArticleResponseDTO ToArticleResponse(this Article post)
        {
            return new ArticleResponseDTO()
            {
                ArticleID = post.ArticleID,
                Title = post.Title,
                ImageUrl = post.ImageUrl,
                MainContent = post.MainContent,
                DateCreated = post.DateCreated,
                DateUpdated = post.DateUpdated,
                CommentAllowing = post.CommentAllowing,
                CategoryID = post.CategoryID, 
                LikeCount = post.LikeCount,
                TimeToRead = post.TimeToRead,
                ShortDescription = post.ShortDescription
            };
        }
    }
}
