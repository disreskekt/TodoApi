namespace Domain.Models;

public class Comment : BaseEntity
{
    public string Text { get; set; }
    
    public int TodoId { get; set; }
    public Todo Todo { get; set; }
}