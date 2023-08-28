using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities.IdentityEntities;
using BlogPost.Core.DTO.IdentityDTO;
using BlogPost.Core.ServiceContracts.IdentityServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BlogPost.Core.Service.IdentityService
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration; 
        private readonly UserManager<ApplicationUser> _userManager;
        public JwtService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
                _configuration = configuration;
                _userManager = userManager;
        }
        public async Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user)
        {
            DateTime expireTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTE"]));
            var userRoles = await _userManager.GetRolesAsync(user);

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            foreach (var role in userRoles)
            {
                claims.Append(new Claim(ClaimTypes.Role, role));
            }


            SymmetricSecurityKey securityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expireTime,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            string token = jwtTokenHandler.WriteToken(tokenGenerator);

            return new AuthenticationResponse()
            {
                FullName = user.FullName,
                Email = user.Email,
                ExpirationDate = expireTime,
                Token = token,
            };
        }
    }
}
