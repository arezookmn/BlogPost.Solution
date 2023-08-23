using BlogPost.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.Domain.Entities
{
    public class Post
    {
        public Guid PostID { get; set; }
        public string Title { get; set; }    
        public string MainContent { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime ? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public bool CommentAllowing { get; set; }

        public int CategoryID { get; set; }

        [Required]
        public Category Category { get; set; }


        //public Guid AuthorID { get; set; }
        // public bool IsPublished { get; set; }
        //public DateTime? DatePublished { get; set; }
        //public int LikeCount { get; set; }
        //public List<TagOptions> Tags { get; set; }
        //public string? Slug { get; set; }



    }
}
