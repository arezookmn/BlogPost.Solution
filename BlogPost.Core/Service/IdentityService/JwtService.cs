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
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BlogPost.Core.Service.IdentityService
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration; 
        public JwtService(IConfiguration configuration)
        {
                _configuration = configuration;
        }
        public async Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user)
        {
            DateTime expireTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTE"]));

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            };

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

            new AuthenticationResponse()
            {
                FullName = user.FullName,
                Email = user.Email,
                ExpirationDate = expireTime,
                Token = token
            };
        }
    }
}
