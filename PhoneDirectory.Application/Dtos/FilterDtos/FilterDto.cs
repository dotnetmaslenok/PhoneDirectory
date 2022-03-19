#nullable enable
namespace PhoneDirectory.Application.Dtos.FilterDtos
{
    public record FilterDto(int? ParentId,
        string? Name);
}