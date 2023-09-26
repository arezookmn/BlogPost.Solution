using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.AuthorDTO;
using BlogPost.Core.Exceptions;
using BlogPost.Core.ServiceContracts.AuthorServiceContracts;
using BlogPost.Core.ServiceContracts.IdentityServiceContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Services.Helper;
using static System.Net.Mime.MediaTypeNames;

namespace BlogPost.Core.Service.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<AuthorService> _logger;
        private readonly ICurrentUserDetails _currentUserDetails;
        public AuthorService(IAuthorRepository authorRepository, ILogger<AuthorService> logger, ICurrentUserDetails currentUserDetails)
        {
            _authorRepository = authorRepository;
            _logger = logger;
            _currentUserDetails = currentUserDetails;
        }
        public async Task<AuthorResponseDTO> AddAuthor(CreateAuthorDTO authorDto)
        {
            try
            {
                if (authorDto == null)
                    throw new ArgumentNullException(message: "Request can not be null", paramName: nameof(authorDto));

                ValidationHelper.ModelValidation(authorDto);
                Guid currentUserId = await _currentUserDetails.GetCurrentUserId();

                Author author = authorDto.ToAuthor();
                author.AuthorId = Guid.NewGuid();
                author.ApplicationUserId = currentUserId;
                Author authorAdded = await _authorRepository.AddAuthor(author);

                AuthorResponseDTO authorResponse = authorAdded.ToAuthorResponseDto();

                return authorResponse;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Null {nameof(authorDto)} object: {ex.Message}");
                throw;
            }
            catch (ArgumentValidationException ex)
            {
                _logger.LogError($"Validation error occurred in {ex.Object}: {ex.Message}");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error occurred: {ex.Message}");
                throw new Exception("An error occurred while adding the author.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding the author.: {ex.Message}");
                throw new Exception($"An error occurred while adding the author.");
            }

        }

        public async Task<AuthorResponseDTO> GetAuthorById(Guid id)
        {
            try
            {
                Author? author = await _authorRepository.GetAuthorById(id);
                if (author == null)
                    throw new EntityNotFoundException("Author", "authorId is invalid");

                AuthorResponseDTO authorResponse = author.ToAuthorResponseDto();
                return authorResponse;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError($"Entity {ex.EntityType} not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting an author {id}: {ex.Message}");
                throw new Exception("An error occurred while getting author.");
            }

        }

        public async Task<AuthorResponseDTO> UpdateAuthor(UpdateAuthorDTO updateAuthorDto)
        {
            try
            {
                if (updateAuthorDto == null)
                    throw new ArgumentNullException(nameof(updateAuthorDto), "request can not be null");

                Author? existingAuthor = await _authorRepository.GetAuthorById(updateAuthorDto.AuthorId);
                if (existingAuthor == null)
                    throw new EntityNotFoundException("Author", "author not exist in database, invalid author id");

                Author toBeUpdatedAuthor = new Author()
                {
                    AuthorId = existingAuthor.AuthorId,
                    ProfileImageUrl = updateAuthorDto.ProfileImageUrl,
                    ShortAbout = updateAuthorDto.ShortAbout
                };

                Author author = await _authorRepository.UpdateAuthor(toBeUpdatedAuthor);

                AuthorResponseDTO updatedAuthor = author.ToAuthorResponseDto();

                return updatedAuthor;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Null {nameof(updateAuthorDto)} object: {ex.Message}");
                throw;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError($"Entity {ex.EntityType} not found: {ex.Message}");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database update error occurred: {ex.Message}");
                throw new Exception("An error occurred while updating the author.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                throw;
            }

        }
}
}
