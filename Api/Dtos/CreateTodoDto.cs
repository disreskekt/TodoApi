using Domain.Enums;

namespace Api.Dtos;

public class CreateTodoDto
{
    public string Header { get; set; }
    public Categories Category { get; set; }
    public Colors Color { get; set; }
}