using BlogPost.Core.Domain.Entities.IdentityEntities;
using BlogPost.Core.DTO.IdentityDTO;
using BlogPost.Core.ServiceContracts.IdentityServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using BlogPost.Core.Enums;

namespace BlogPost.WebApi.Controllers.AccountController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;   
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;   
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
                var authenticationResponse = _jwtService.CreateJwtToken(user);

                return Ok(authenticationResponse);
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

        [HttpPost("register/user")]
        public async Task<ActionResult<ApplicationUser>> PostRegisterUser([FromBody] RegisterDTO registerDto)
        {
            return await RegisterUserOrAuthor(registerDto, "user");
        }


        [HttpPost("register/author")]
        public async Task<ActionResult<ApplicationUser>> RegisterAuthor([FromBody] RegisterDTO registerDto)
        {
            return await RegisterUserOrAuthor(registerDto, "author");
        }



        private async Task<ActionResult<ApplicationUser>> RegisterUserOrAuthor(RegisterDTO registerDto, string roleName)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(t => t.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

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
                await CheckAndAddRoleAsync(user, roleName);
                await _signInManager.SignInAsync(user: user, isPersistent: false);
                var authenticationResponse = _jwtService.CreateJwtToken(user);

                return Ok(authenticationResponse);
            }
            else
            {
                string errorResult = String.Join(",", result.Errors.Select(e => e.Description));
                return Problem(errorResult);
            }
        }


        private async Task CheckAndAddRoleAsync(ApplicationUser user, string roleName)
        {
            //todo:check that if roleName contains in UserTypeOptions

            var resultRole = await _roleManager.FindByNameAsync(roleName);

            if (resultRole == null)
            {
                var newRole = new ApplicationRole() { Name = roleName };
                await _roleManager.CreateAsync(newRole);
            }

            await _userManager.AddToRoleAsync(user, roleName);
        }


    }
}
