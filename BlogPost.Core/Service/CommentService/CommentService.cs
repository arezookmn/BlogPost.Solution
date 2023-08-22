using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.DTO.CommentDTO;
using BlogPost.Core.ServiceContracts.CommentServicesInterface;
using Services.Helper;

namespace BlogPost.Core.Service.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository; 
        }

        public async Task<CommentResponseDTO> CreateComment(CreateCommentRequestDTO commentRequestDTO)
        {
            if (commentRequestDTO == null) throw new ArgumentException(); //todo:Add custom exception 
            ValidationHelper.ModelValidation(commentRequestDTO);
            //todo:business Validation

            Comment commnetFromRequest = commentRequestDTO.ToComment();
            commnetFromRequest.CommentID = new Guid();
            commnetFromRequest.CreatedDate = DateTime.UtcNow;  

            Comment createdComment = await _commentRepository.CreateComment(commnetFromRequest);

            return createdComment.ToCommentResponseDto();
        }

        public async Task<bool> DeleteComment(Guid commentId)
        {
            Comment? CommentFromRepository = await _commentRepository.GetCommentById(commentId);

            if(CommentFromRepository == null) return false; //todo:fixing NotFound condition

            await _commentRepository.DeleteComment(CommentFromRepository); 
            return true;
        }
    }
}
