using BlogPost.Core.DTO.AuthorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;

namespace BlogPost.Core.Domain.RepositoryContracts
{
    public interface IAuthorRepository
    {
        Task<Author> AddAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task<Author?> GetAuthorById(Guid id);
    }
}
