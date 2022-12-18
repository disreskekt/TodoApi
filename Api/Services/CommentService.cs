using Api.Dtos;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Api.Services;

public class CommentService : ICommentService
{
    private readonly IRepository<Comment> _commentRepository;
    private readonly IMapper _mapper;
    private readonly ITodoService _todoService;

    public CommentService(IRepository<Comment> commentRepository, IMapper mapper, ITodoService todoService)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _todoService = todoService;
    }
    
    public IEnumerable<CommentDto> GetByTodoId(int todoId)
    {
        IQueryable<Comment> comments = _commentRepository.GetAll(comment => comment.TodoId == todoId);
        
        return _mapper.Map<IEnumerable<CommentDto>>(comments.AsEnumerable());
    }
    
    public async Task<int> Create(CreateCommentDto commentDto)
    {
        await _todoService.Get(commentDto.TodoId);
        
        Comment newComment = new Comment
        {
            TodoId = commentDto.TodoId,
            Text = commentDto.Text
        };
        
        _commentRepository.Insert(newComment);
        await _commentRepository.SaveChangesAsync();
        
        return newComment.Id;
    }
    
    public async Task<CommentDto> Get(int id)
    {
        Comment? comment = await _commentRepository.Get(id);
        
        if (comment is null)
        {
            throw new EntityNotFoundException();
        }
        
        return _mapper.Map<CommentDto>(comment);
    }
}