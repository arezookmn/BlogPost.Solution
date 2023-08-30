using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.DTO.CommentDTO;

namespace BlogPost.Core.ServiceContracts.CommentServicesInterface
{
    public interface ICommentService
    {
        Task<CommentResponseDTO> CreateComment(CreateCommentRequestDTO commentRequestDTO);

        Task<bool> DeleteComment(Guid commentId);

        Task<List<CommentResponseDTO>> GetAllCommentsOfSpecificArticle(Guid articleId);

        Task<CommentResponseDTO> UpdateComment(Guid commentId, UpdateCommentRequestDTO requestDto);

    }
}
