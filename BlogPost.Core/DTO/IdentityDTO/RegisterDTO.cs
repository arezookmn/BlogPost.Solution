using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Enums;

namespace BlogPost.Core.DTO.IdentityDTO
{
    public class RegisterDTO
    {

        [Required(ErrorMessage = "Name cant be blank")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email cant be blank")]
        [EmailAddress(ErrorMessage = "Your email not in correct format")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already in use")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain numbers only")]
        [DataType(DataType.PhoneNumber)]
        [Remote("IsPhoneNumberAvailable", "Account", ErrorMessage = "Phone number is already in use.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password cant be blank")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password cant be blank")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow; // Default value set to current UTC time

        [Required]
        public string Role { get; set; } = UserTypeOptions.User.ToString();

    }
}
