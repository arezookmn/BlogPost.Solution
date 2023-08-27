using BlogPost.Core.Domain.Entities.IdentityEntities;
using BlogPost.Core.DTO.IdentityDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.WebApi.Controllers.AccountController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> PostRegister([FromBody] RegisterDTO registerDto)
        {
            //validation
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(",",ModelState.Values.SelectMany(t => t.Errors).Select(e => e.ErrorMessage));

                return Problem(errorMessage);
            }

            //create user
            ApplicationUser user = new ApplicationUser()
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email,
                RegistrationDate = registerDto.RegistrationDate
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user:user, isPersistent:false);

                return Ok(user);
            }
            else
            {
                string errorResult = String.Join(",", result.Errors.Select(e => e.Description));

                return Problem(errorResult);
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> PostLogin(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(t => t.Errors).Select(e => e.ErrorMessage));

                return Problem(errorMessage);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent:true, lockoutOnFailure:false);

            if (result.Succeeded)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return NoContent();
                }
                return Ok(new {personName = user.FullName , email = user.Email});
            }

            return Problem("Invalid Email or Password");
        }

        [HttpGet("logout")]
        public async Task<ActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string emailAddress)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress);
            if (user != null)
            {
                return Ok(false); 
            }
            return Ok(true);

        }

        [HttpGet]
        public async Task<IActionResult> IsPhoneNumberAvailable(string phoneNumber)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(t => t.PhoneNumber == phoneNumber);
            return Ok(user == null);
        }




    }
}
