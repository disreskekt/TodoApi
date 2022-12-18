using Domain.Models;

namespace Api.Dtos;

public class TodoDto : Todo
{
    public string Hash { get; set; }
    new public IEnumerable<CommentDto> Comments { get; set; }
}