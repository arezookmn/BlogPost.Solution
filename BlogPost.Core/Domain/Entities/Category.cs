using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Enums;

namespace BlogPost.Core.Domain.Entities
{
    public class Category
    {
        public int CategoryID { get; set; } 
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();

    }
}
