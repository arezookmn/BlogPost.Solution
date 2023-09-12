using BlogPost.Core.DTO.AuthorDTO;
using BlogPost.Core.Exceptions;
using BlogPost.Core.ServiceContracts.AuthorServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.WebApi.Controllers.AuthorController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;
        public AuthorController(IAuthorService authorService,ILogger<AuthorController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<AuthorResponseDTO>> PostAuthor(CreateAuthorDTO createAuthorDto)
        {
            try
            {
                AuthorResponseDTO authorResponse = await _authorService.AddAuthor(createAuthorDto);
                return CreatedAtAction(nameof(GetAuthor), new { id = authorResponse.AuthorId }, authorResponse);
            }

            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Null {nameof(createAuthorDto)} object: {ex.Message}");
                return BadRequest("Invalid request data.");
            }
            catch (ArgumentValidationException ex)
            {
                _logger.LogError($"Validation error occurred: {ex.Message}");
                return BadRequest("Invalid request data.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while adding the author.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred while adding the author.");
            }

        }

        [HttpGet("{authorId}")]
        public async Task<ActionResult<AuthorResponseDTO>> GetAuthor(Guid authorId)
        {
            try
            {
                AuthorResponseDTO authorResponse = await _authorService.GetAuthorById(authorId);
                return Ok(authorResponse);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError($"Entity {ex.EntityType} not found: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting an author {authorId}: {ex.Message}");
                return StatusCode(500, "An error occurred while getting author.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<AuthorResponseDTO>> PutAuthor(UpdateAuthorDTO updateAuthorDto)
        {
            try
            {
                if (updateAuthorDto == null)
                    throw new ArgumentNullException(nameof(updateAuthorDto), "Request can not be null");

                AuthorResponseDTO updatedAuthor = await _authorService.UpdateAuthor(updateAuthorDto);
                return Ok(updatedAuthor);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Null {nameof(updateAuthorDto)} object: {ex.Message}");
                return BadRequest("Invalid request data");
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError($"Entity {ex.EntityType} not found: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database update error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the author.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

    }
}
