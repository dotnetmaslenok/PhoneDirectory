using System.ComponentModel.DataAnnotations;

namespace PhoneDirectory.Application.Dtos.CreateDtos
{
    public record CreateUserDto(
        [Required]
        [StringLength(20, MinimumLength = 2)]
        string Name,
        [Required]
        bool IsChief,
        [Range(1, int.MaxValue)]
        int DivisionId);
}