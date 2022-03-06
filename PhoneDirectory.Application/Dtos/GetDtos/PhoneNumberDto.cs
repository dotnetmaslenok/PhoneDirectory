using System.ComponentModel.DataAnnotations;
using PhoneDirectory.Application.BaseDtos;

namespace PhoneDirectory.Application.Dtos.GetDtos
{
    public record PhoneNumberDto(int Id,
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{4}\)?[\s.-]\d{3}[\s.-]\d{2}[\s.-]\d{2}$")]
        string UserPhoneNumber,
        int UserId,
        ApplicationUserDto User) : BaseDto(Id);
}