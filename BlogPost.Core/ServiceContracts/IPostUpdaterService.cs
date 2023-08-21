using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.DTO;

namespace BlogPost.Core.ServiceContracts
{
    public interface IPostUpdaterService
    {
        Task<PostResponseDTO> UpdatePost(UpdatePostRequestDTO requestDto);
    }
}
