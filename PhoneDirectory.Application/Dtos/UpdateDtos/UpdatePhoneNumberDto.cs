using System.ComponentModel.DataAnnotations;
using PhoneDirectory.Application.BaseDtos;

namespace PhoneDirectory.Application.Dtos.UpdateDtos
{
    public record UpdatePhoneNumberDto(
        [Required]
        [Range(1, int.MaxValue)]
        int Id,
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{4}\)?[\s.-]\d{3}[\s.-]\d{2}[\s.-]\d{2}$")]
        string UserPhoneNumber,
        [Required]
        [Range(1, int.MaxValue)]
        int UserId) : BaseDto(Id);
}