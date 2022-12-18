using Api.Dtos;

namespace Api.Services.Interfaces;

public interface ICommentService
{
    public Task<IEnumerable<CommentDto>> GetByTodoId(int todoId);
    public Task<int> Create(CreateCommentDto commentDto);
    public Task<CommentDto> Get(int id);
}