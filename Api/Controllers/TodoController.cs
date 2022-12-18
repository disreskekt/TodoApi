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
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        try
        {
            await _todoService.Delete(id);
            
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpPatch]
    public async Task<IActionResult> UpdateHeader([FromBody] UpdateHeaderDto updateHeaderDto)
    {
        try
        {
            await _todoService.UpdateHeader(updateHeaderDto);
            
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetComments([FromQuery] int todoId)
    {
        try
        {
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}