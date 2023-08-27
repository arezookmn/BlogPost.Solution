using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.DTO.IdentityDTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email cant be blank")]
        [EmailAddress(ErrorMessage = "Your email not in correct format")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password cant be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
