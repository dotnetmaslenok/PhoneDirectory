using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhoneDirectory.Application.Dtos.GetDtos;

namespace PhoneDirectory.Application.Dtos.CreateDtos
{
    public record CreateDivisionDto(
        [Required]
        [StringLength(50, MinimumLength = 2)]
        string Name,
        List<CreateDivisionDto> Divisions);
}