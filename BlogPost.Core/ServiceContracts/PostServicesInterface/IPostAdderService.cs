using BlogPost.Core.DTO.PostDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.ServiceContracts.PostServicesInterface
{
    public interface IPostAdderService
    {
        Task<PostResponseDTO> CreatePostAsync(CreatePostRequestDTO requestDto);

    }
}
