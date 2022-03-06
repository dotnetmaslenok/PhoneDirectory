using System.Collections.Generic;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.UnitTests.DtoHelpers
{
    public static class UserDtoHelper
    {
        public static ApplicationUserDto GetOne()
        {
            return new ApplicationUserDto(1, "TestUser1", false, 1,
                new DivisionDto(1, "TestDivision1", null, null), new List<PhoneNumberDto>());
        }
    }
}