using System.ComponentModel.DataAnnotations;
using PhoneDirectory.Application.BaseDtos;

namespace PhoneDirectory.Application.Dtos.UpdateDtos
{
    public record UpdateDivisionDto(
        [Required]
        [Range(1, int.MaxValue)]
        int Id,
        [Required]
        [StringLength(50, MinimumLength = 2)]
        string Name) : BaseDto(Id);
}