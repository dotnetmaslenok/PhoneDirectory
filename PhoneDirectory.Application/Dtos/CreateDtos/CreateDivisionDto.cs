using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneDirectory.Application.Dtos.CreateDtos
{
    public record CreateDivisionDto(
        [Required]
        [StringLength(50, MinimumLength = 2)]
        string Name,
        int? ParentId,
        List<CreateDivisionDto> ChildDivisions);
}