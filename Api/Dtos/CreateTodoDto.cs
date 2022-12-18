using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Api.Dtos;

public class CreateTodoDto
{
    [Required]
    [MaxLength(100)]
    public string Header { get; set; }
    [Required]
    public Categories Category { get; set; }
    [Required]
    public Colors Color { get; set; }
}