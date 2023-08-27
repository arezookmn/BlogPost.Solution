using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.DTO.IdentityDTO
{
    public class AuthenticationResponse
    {
        public string? FullName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty; 
        public DateTime? ExpirationDate { get; set;}

    }
}
