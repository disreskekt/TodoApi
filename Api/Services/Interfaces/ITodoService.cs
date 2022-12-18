using Api.Dtos;

namespace Api.Services.Interfaces;

public interface ITodoService
{
    public IEnumerable<TodoDto> GetAll();
    public Task<int> Create(CreateTodoDto todoDto);
    public Task<TodoDto> Get(int id);
    public Task<TodoDto> GetIncludeComments(int id);
    public Task Delete(int id);
    public Task UpdateHeader(UpdateHeaderDto updateHeaderDto);
}