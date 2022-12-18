using Domain.Enums;

namespace Domain.Models;

public class Todo : BaseEntity
{
    public string Header { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDone { get; set; }
    public Categories Category { get; set; }
    public Colors Color { get; set; }
    public ICollection<Comment> Comments { get; set; }
}