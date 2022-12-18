using Api.Dtos;

namespace Api.Services.Interfaces;

public interface ICommentService
{
    public IEnumerable<CommentDto> GetComments(int todoId);
}