﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.DTO.PostDTO;

namespace BlogPost.Core.ServiceContracts.PostServicesInterface
{
    public interface IPostUpdaterService
    {
        Task<PostResponseDTO> UpdatePost(UpdatePostRequestDTO requestDto);
    }
}