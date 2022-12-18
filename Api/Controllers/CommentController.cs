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
    
    [HttpGet]
    public IActionResult GetByTodoId([FromQuery] int todoId)
    {
        try
        {
            IEnumerable<CommentDto> commentDtos = _commentService.GetByTodoId(todoId);
            
            return Ok(commentDtos);
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
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int id)
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