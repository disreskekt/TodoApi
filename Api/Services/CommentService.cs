using Api.Dtos;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Api.Services;

public class CommentService : ICommentService
{
    private readonly IRepository<Comment> _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(IRepository<Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }
    
    public IEnumerable<CommentDto> GetComments(int todoId)
    {
        IQueryable<Comment> comments = _commentRepository.GetAll(comment => comment.TodoId == todoId);
        
        return _mapper.Map<IEnumerable<CommentDto>>(comments.AsEnumerable());
    }
}