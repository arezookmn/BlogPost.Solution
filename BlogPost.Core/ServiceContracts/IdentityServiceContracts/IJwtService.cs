using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities.IdentityEntities;
using BlogPost.Core.DTO.IdentityDTO;

namespace BlogPost.Core.ServiceContracts.IdentityServiceContracts
{
    public interface IJwtService
    {
        Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user);
    }
}
