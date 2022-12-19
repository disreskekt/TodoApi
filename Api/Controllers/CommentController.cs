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
    private readonly ILogger<CommentController> _logger;

    public CommentController(ICommentService commentService, ILogger<CommentController> logger)
    {
        _commentService = commentService;
        _logger = logger;
    }
    
    [HttpGet("{todoId}")]
    public async Task<IActionResult> GetByTodoId([FromRoute] int todoId)
    {
        try
        {
            _logger.LogInformation("Called {CommentController}/{GetByTodoId}", nameof(CommentController), nameof(GetByTodoId));
            
            IEnumerable<CommentDto> commentDtos = await _commentService.GetByTodoId(todoId);
            
            _logger.LogInformation("Ok for {CommentController}/{GetBuTodoId}", nameof(CommentController), nameof(GetByTodoId));
            
            return Ok(commentDtos);
        }
        catch (EntityNotFoundException)
        {
            _logger.LogInformation("NotFound for {CommentController}/{GetBuTodoId}", nameof(CommentController), nameof(GetByTodoId));
            return NotFound();
        }
        catch (Exception)
        {
            _logger.LogInformation("Badrequest for {CommentController}/{GetBuTodoId}", nameof(CommentController), nameof(GetByTodoId));
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentDto commentDto)
    {
        try
        {
            _logger.LogInformation("Called {CommentController}/{Create}", nameof(CommentController), nameof(Create));
            
            int id = await _commentService.Create(commentDto);
            
            _logger.LogInformation("Created for {CommentController}/{Create}", nameof(CommentController), nameof(Create));
            return CreatedAtAction(nameof(Get), new { Id = id}, commentDto);
        }
        catch (Exception)
        {
            _logger.LogInformation("Badrequest for {CommentController}/{Create}", nameof(CommentController), nameof(Create));
            return BadRequest();
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            _logger.LogInformation("Called {CommentController}/{Get}", nameof(CommentController), nameof(Get));
            
            CommentDto commentDto = await _commentService.Get(id);
            
            _logger.LogInformation("Ok for {CommentController}/{Get}", nameof(CommentController), nameof(Get));
            return Ok(commentDto);
        }
        catch (EntityNotFoundException)
        {
            _logger.LogInformation("NotFound for {CommentController}/{Get}", nameof(CommentController), nameof(Get));
            return NotFound();
        }
        catch (Exception)
        {
            _logger.LogInformation("Badrequest for {CommentController}/{Get}", nameof(CommentController), nameof(Get));
            return BadRequest();
        }
    }
}