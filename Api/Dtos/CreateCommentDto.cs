namespace Api.Dtos;

public class CreateCommentDto
{
    public int TodoId { get; set; }
    public string Text { get; set; }
}