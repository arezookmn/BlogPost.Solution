using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Enums;

namespace BlogPost.Core.DTO.CategoryDTO
{
    public class CreateCategoryDTO
    {
        public CategoryNameOption CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public Category ToCategory()
        {
            return new Category
            {
                CategoryName = CategoryName.ToString(),
                CategoryDescription = CategoryDescription
            };
        }
    }
}
