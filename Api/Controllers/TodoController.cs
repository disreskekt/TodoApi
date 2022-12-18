using Api.Dtos;
using Api.Services.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            IEnumerable<TodoDto> todoDtos = _todoService.GetAll();
            
            return Ok(todoDtos);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoDto todoDto)
    {
        try
        {
            int id = await _todoService.Create(todoDto);
            
            return CreatedAtAction(nameof(Get), new { Id = id}, todoDto);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        try
        {
            TodoDto todoDto = await _todoService.Get(id);

            return Ok(todoDto);
        }
        catch (EntityNotFoundException)
        {
            return NoContent();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}