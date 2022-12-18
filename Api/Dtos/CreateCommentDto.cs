using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class CreateCommentDto
{
    [Required]
    public int TodoId { get; set; }
    [Required]
    [MaxLength(255)]
    public string Text { get; set; }
}