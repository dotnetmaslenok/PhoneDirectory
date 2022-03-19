using System.Collections.Generic;
using PhoneDirectory.Application.BaseDtos;

namespace PhoneDirectory.Application.Dtos.GetDtos
{
    public record DivisionDto(
        int Id,
        string Name,
        List<ApplicationUserDto> Users,
        int ParentDivisionId,
        DivisionDto ParentDivision,
        List<DivisionDto> ChildDivisions) : BaseDto(Id);
}