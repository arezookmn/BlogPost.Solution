using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.DTO.AuthorDTO
{
    public class AuthorResponseDTO
    {
        public Guid AuthorId { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string ShortAbout { get; set; }
    }
}
