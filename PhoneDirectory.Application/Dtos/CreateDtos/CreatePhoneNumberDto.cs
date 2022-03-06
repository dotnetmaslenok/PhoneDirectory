using System.ComponentModel.DataAnnotations;
using PhoneDirectory.Application.Dtos.GetDtos;

namespace PhoneDirectory.Application.Dtos.CreateDtos
{
    public record CreatePhoneNumberDto(
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{4}\)?[\s.-]\d{3}[\s.-]\d{2}[\s.-]\d{2}$")]
        string UserPhoneNumber,
        [Required]
        [Range(1, int.MaxValue)]
        int UserId);
}