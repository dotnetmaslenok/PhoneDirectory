using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhoneDirectory.Application.BaseDtos;

namespace PhoneDirectory.Application.Dtos.GetDtos
{
    public record DivisionDto(
        int Id,
        string Name,
        List<ApplicationUserDto> Users,
        List<DivisionDto> Divisions) : BaseDto(Id);
}