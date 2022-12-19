using System.ComponentModel.DataAnnotations;
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
    private readonly ILogger<TodoController> _logger;

    public TodoController(ITodoService todoService, ILogger<TodoController> logger)
    {
        _todoService = todoService;
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult GetAll([FromQuery] [MaxLength(100)] string? textToFindInHeader = null, [FromQuery] params int[] ids)
    {
        try
        {
            _logger.LogInformation("Called {GetAll}", nameof(GetAll));
            
            IEnumerable<TodoDto> todoDtos = _todoService.GetAll(textToFindInHeader, ids);
            
            _logger.LogInformation("Ok for {GetAll}", nameof(GetAll));
            
            return Ok(todoDtos);
        }
        catch (Exception)
        {
            _logger.LogWarning("Badrequest for {GetAll}", nameof(GetAll));
            return BadRequest();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoDto todoDto)
    {
        try
        {
            _logger.LogInformation("Called {Create}", nameof(Create));
            
            int id = await _todoService.Create(todoDto);
            
            _logger.LogInformation("Created for {Create}", nameof(Create));
            
            return CreatedAtAction(nameof(Get), new { Id = id}, todoDto);
        }
        catch (Exception)
        {
            _logger.LogWarning("Badrequest for {Create}", nameof(Create));
            return BadRequest();
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            _logger.LogInformation("Called {Get}", nameof(Get));
            
            TodoDto todoDto = await _todoService.GetIncludeComments(id);
            
            _logger.LogInformation("Ok for {Get}", nameof(Get));
            
            return Ok(todoDto);
        }
        catch (EntityNotFoundException)
        {
            _logger.LogWarning("NotFound for {Get}", nameof(Get));
            return NotFound();
        }
        catch (Exception)
        {
            _logger.LogWarning("Badrequest for {Get}", nameof(Get));
            return BadRequest();
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            _logger.LogInformation("Called {Delete}", nameof(Delete));
            
            await _todoService.Delete(id);
            
            _logger.LogInformation("NoContent for {Delete}", nameof(Delete));
            
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            _logger.LogWarning("NotFound for {Delete}", nameof(Delete));
            return NotFound();
        }
        catch (Exception)
        {
            _logger.LogWarning("Badrequest for {Delete}", nameof(Delete));
            return BadRequest();
        }
    }
    
    [HttpPatch]
    public async Task<IActionResult> UpdateHeader([FromBody] UpdateHeaderDto updateHeaderDto)
    {
        try
        {
            _logger.LogInformation("Called {UpdateHeader}", nameof(UpdateHeader));
            
            await _todoService.UpdateHeader(updateHeaderDto);
            
            _logger.LogInformation("NoContent for {UpdateHeader}", nameof(UpdateHeader));
            
            return NoContent();
        }
        catch (NothingToUpdateException)
        {
            _logger.LogInformation("NoContent for {UpdateHeader}", nameof(UpdateHeader));
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            _logger.LogWarning("NotFound for {UpdateHeader}", nameof(UpdateHeader));
            return NotFound();
        }
        catch (Exception)
        {
            _logger.LogWarning("Badrequest for {UpdateHeader}", nameof(UpdateHeader));
            return BadRequest();
        }
    }
}