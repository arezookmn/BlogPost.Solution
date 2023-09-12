using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.DTO.AuthorDTO;
using BlogPost.Core.ServiceContracts.AuthorServiceContracts;

namespace BlogPost.Core.Service.AuthorService
{
    public class AuthorService : IAuthorService
    {
        public Task<AuthorResponseDTO> AddAuthor(CreateAuthorDTO authorDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuthorResponseDTO> GetAuthorById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AuthorResponseDTO> UpdateAuthor(UpdateAuthorDTO authorDto)
        {
            throw new NotImplementedException();
        }
    }
}
