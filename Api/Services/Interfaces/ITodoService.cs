using Api.Dtos;

namespace Api.Services.Interfaces;

public interface ITodoService
{
    public IEnumerable<TodoDto> GetAll();
    public Task<int> Create(CreateTodoDto todoDto);
    public Task<TodoDto> Get(int id);
}