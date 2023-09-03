using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities.IdentityEntities;
using BlogPost.Core.ServiceContracts.IdentityServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BlogPost.Core.Service.IdentityService
{
    public class CurrentUserDetail : ICurrentUserDetails
    {
        private  readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserDetail(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> GetCurrentUserId()
        {
            // Get the current HttpContext
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null && httpContext.User.Identity.IsAuthenticated)
            {
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    var user = await _userManager.FindByIdAsync(userId.ToString());
                    if (user != null)
                    {
                        return userId;
                    }
                }
            }

            throw new InvalidOperationException("User ID not found or user is not authenticated.");

        }
    }
}
