using Api.Dtos;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Api.Services;

public class TodoService : ITodoService
{
    private readonly IRepository<Todo> _todoRepository;
    private readonly IMapper _mapper;

    public TodoService(IRepository<Todo> todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public IEnumerable<TodoDto> GetAll()
    {
        IEnumerable<Todo> todos = _todoRepository.GetAll(todo => todo.Comments);

        return _mapper.Map<IEnumerable<TodoDto>>(todos);
    }

    public async Task<int> Create(CreateTodoDto todoDto)
    {
        Todo newTodo = new Todo
        {
            Header = todoDto.Header,
            Category = todoDto.Category,
            Color = todoDto.Color,
            CreationDate = DateTime.Now,
            IsDone = false
        };
        
        _todoRepository.Insert(newTodo);
        await _todoRepository.SaveChangesAsync();
        
        return newTodo.Id;
    }

    public async Task<TodoDto> Get(int id)
    {
        Todo? todo = await _todoRepository.Get(id);
        
        if (todo is null)
        {
            throw new EntityNotFoundException();
        }
        
        return _mapper.Map<TodoDto>(todo);
    }
}