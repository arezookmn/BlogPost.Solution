using BlogPost.Core.Domain.IdentityEntities;
using BlogPost.Core.DTO.IdentityDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost]
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


    }
}
