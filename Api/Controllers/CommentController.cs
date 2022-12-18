using Api.Dtos;
using Api.Services.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpGet("{todoId}")]
    public async Task<IActionResult> GetByTodoId([FromRoute] int todoId)
    {
        try
        {
            IEnumerable<CommentDto> commentDtos = await _commentService.GetByTodoId(todoId);
            
            return Ok(commentDtos);
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentDto commentDto)
    {
        try
        {
            int id = await _commentService.Create(commentDto);
            
            return CreatedAtAction(nameof(Get), new { Id = id}, commentDto);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            CommentDto commentDto = await _commentService.Get(id);
            
            return Ok(commentDto);
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
}