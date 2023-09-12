using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.DTO.AuthorDTO
{
    public class AuthorResponseDTO
    {
        public Guid AuthorId { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string ShortAbout { get; set; }
    }

    public static class AuthorExtension
    {
        public static AuthorResponseDTO ToAuthorResponseDto(this Author author)
        {
            return new AuthorResponseDTO()
            {
                AuthorId = author.AuthorId,
                ProfileImageUrl = author.ProfileImageUrl,
                ShortAbout = author.ShortAbout
            };
        }
    }
}
