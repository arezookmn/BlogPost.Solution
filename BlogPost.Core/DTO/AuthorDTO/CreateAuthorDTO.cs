using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.DTO.AuthorDTO
{
    public class CreateAuthorDTO
    {
        public string? ProfileImageUrl { get; set; }
        [Required(ErrorMessage = "ShortAbout is required")]
        public string ShortAbout { get; set; }

        public Author ToAuthor()
        {
            return new Author()
            {
                ProfileImageUrl = ProfileImageUrl,
                ShortAbout = ShortAbout
            };
        }
    }

}
