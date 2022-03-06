using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhoneDirectory.Application.Dtos.GetDtos;

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