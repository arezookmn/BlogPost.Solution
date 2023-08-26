using BlogPost.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.Domain.Entities
{
    public class Comment
    {
        public Guid CommentID { get; set; }
        public Guid PostID { get; set; }
        public string CommentText { get; set; }
        public string? NameOfCommentAuthor { get; set; } = "Unknown";
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }


        //navigation property
        public Post? Post { get; set; }

        //public Guid CommentAuthorID { get; set; }


        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
