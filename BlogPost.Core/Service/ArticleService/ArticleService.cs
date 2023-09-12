using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.PostDTO;
using BlogPost.Core.Exceptions;
using BlogPost.Core.ServiceContracts.ArticleServiceContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Helper;

namespace BlogPost.Core.Service.ArticleService
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<ArticleService> _logger;

        public ArticleService(IArticleRepository articleRepository, ILogger<ArticleService> logger)
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }

        public async Task DeleteArticleAsync(Guid articleId)
        {
            try
            {
                Article articleFromGet = await _articleRepository.GetArticleByIdAsync(articleId);

                if (articleFromGet == null)
                {
                    _logger.LogError($"Article with ID {articleId} not found.");
                    throw new EntityNotFoundException("Article", "articleId is invalid !");
                }

                await _articleRepository.DeleteArticleAsync(articleFromGet);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError($"Entity {ex.EntityType} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting article with ID {articleId}: {ex.Message}");
                throw;
            }
        }

        public async Task SoftDeleteArticleAsync(Guid articleId)
        {
            try
            {
                Article articleFromGet = await _articleRepository.GetArticleByIdAsync(articleId);

                if (articleFromGet == null)
                    throw new EntityNotFoundException("Article", "articleId is invalid !");

                await _articleRepository.SoftDeleteArticleAsync(articleFromGet);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError($"Entity {ex.EntityType} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting article with ID {articleId}: {ex.Message}");
                throw;
            }

        }

        public async Task<ArticleResponseDTO> CreateArticleAsync(CreateArticleRequestDTO requestDto)
        {
            try
            {
                if (requestDto == null)
                    throw new ArgumentNullException(nameof(requestDto), "Request can not be null");

                ValidationHelper.ModelValidation(requestDto);

                //Business validation //todo:adding business validation for adding article, and throw custom business exception
                //BusinessExceptions can inherit form validation exception
                Article articleFromRequest = requestDto.ToArticle();

                articleFromRequest.ArticleID = Guid.NewGuid();
                articleFromRequest.DateCreated = DateTime.UtcNow;

                Article articleFromRepository = await _articleRepository.AddArticleAsync(articleFromRequest);

                ArticleResponseDTO articleResponseDTO = articleFromRepository.ToArticleResponse();

                return articleResponseDTO;

            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Null CreateRequestDto object {ex.Message}");
                throw;

            }
            catch (ArgumentValidationException ex)
            {
                _logger.LogError($"Validation error occurred in validating request object {ex.Object}: {ex.Message}");
                throw;

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while Creating article  {requestDto}: {ex.Message}");
                throw new Exception("An error occurred while creating the article.");
            }
        }

        public async Task<ArticleResponseDTO> GetArticleByIdAsync(Guid articleId)
        {
            try
            {
                Article articleFromGet = await _articleRepository.GetArticleByIdAsync(articleId);

                if (articleFromGet == null)
                    throw new EntityNotFoundException("Article", "articleId is invalid !");

                ArticleResponseDTO articleResponse = articleFromGet.ToArticleResponse();

                return articleResponse;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError($"Entity {ex.EntityType} not found: {ex.Message}");
                throw;        // Rethrow the exception for further handling if needed
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting an article {articleId}: {ex.Message}");
                throw new Exception("An error occurred while getting the article.");
            }
        }

        public async Task<ArticleResponseDTO> UpdateArticle(UpdateArticleRequestDTO updateRequestDto)
        {
            try
            {
                if (updateRequestDto == null)
                    throw new ArgumentNullException(nameof(updateRequestDto), "request can not be null");

                ValidationHelper.ModelValidation(updateRequestDto);
                //todo: add business validation : for example checking is the post is not spam 

                ArticleResponseDTO articleResponseFromGet = await GetArticleByIdAsync(updateRequestDto.ArticleID);

                Article articleToBeUpdated = new Article()
                {
                    ArticleID = articleResponseFromGet.ArticleID,
                    Title = updateRequestDto.Title,
                    MainContent = updateRequestDto.MainContent,
                    ImageUrl = updateRequestDto.ImageUrl,
                    DateCreated = articleResponseFromGet.DateCreated,
                    DateUpdated = articleResponseFromGet.DateUpdated
                };

                Article updatedArticle = await _articleRepository.UpdateArticleAsync(articleToBeUpdated);

                ArticleResponseDTO articleResponse = updatedArticle.ToArticleResponse();

                return articleResponse;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Null {nameof(updateRequestDto)} object {ex.Message}");
                throw;
            }
            catch (ArgumentValidationException ex)
            {
                _logger.LogError($"Validation error occurred in {ex.Object}: {ex.Message}");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error occurred while Updating {updateRequestDto}: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating an article {updateRequestDto}: {ex.Message}");
                throw new Exception("An error occurred while Updating the article.");
            }
        }
    }
}
