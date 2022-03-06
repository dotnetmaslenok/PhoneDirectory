using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhoneDirectory.Application.BaseDtos;

namespace PhoneDirectory.Application.Dtos.GetDtos
{
    public record ApplicationUserDto(
        int Id,
        string Name,
        bool IsChief,
        int DivisionId,
        DivisionDto Division,
        List<PhoneNumberDto> PhoneNumbers) : BaseDto(Id);
}