using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class UpdateHeaderDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string NewHeader { get; set; }
}