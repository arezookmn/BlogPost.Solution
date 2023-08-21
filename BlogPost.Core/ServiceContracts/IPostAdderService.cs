using BlogPost.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.ServiceContracts
{
    public interface IPostAdderService
    {
        Task<PostResponseDTO> CreatePostAsync(CreatePostRequestDTO requestDto);

    }
}
