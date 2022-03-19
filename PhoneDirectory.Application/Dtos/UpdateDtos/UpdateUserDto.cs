using System.ComponentModel.DataAnnotations;
using PhoneDirectory.Application.BaseDtos;

namespace PhoneDirectory.Application.Dtos.UpdateDtos
{
    public record UpdateUserDto(
        [Required]
        [Range(1, int.MaxValue)]
        int Id,
        [StringLength(50, MinimumLength = 2)]
        string Name,
        [Required]
        bool IsChief,
        [Required]
        [Range(1, int.MaxValue)]
        int DivisionId) : BaseDto(Id);
}