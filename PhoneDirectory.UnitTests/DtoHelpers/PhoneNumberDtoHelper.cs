using System.Collections.Generic;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.UnitTests.DtoHelpers
{
    public static class PhoneNumberDtoHelper
    {
        public static PhoneNumberDto GetOne()
        {
            return new PhoneNumberDto(1, "(1234)-567-89-11", 1, null);
        }
    }
}