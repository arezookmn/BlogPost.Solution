using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.DTO.AuthorDTO;

namespace BlogPost.Core.ServiceContracts.AuthorServiceContracts
{
    public interface IAuthorService
    {
        Task<AuthorResponseDTO> AddAuthor(CreateAuthorDTO authorDto);
        Task<AuthorResponseDTO> UpdateAuthor(UpdateAuthorDTO authorDto);
        Task<AuthorResponseDTO> GetAuthorById(Guid id);
    }
}
