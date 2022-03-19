using System;
using System.Collections.Generic;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;

namespace PhoneDirectory.UnitTests.DtoHelpers
{
    public static class UserDtoHelper
    {
        public static ApplicationUserDto GetOneDefaultDto()
        {
            return new ApplicationUserDto(1, "TestUser1", false, 1,
                DivisionDtoHelper.GetOneDefaultDto(), new List<PhoneNumberDto>()
                {
                    new(1, "(1234)-567-89-11", 1, null)
                });
        }

        public static List<ApplicationUserDto> GetManyDefauldDtos()
        {
            var userDtos = new List<ApplicationUserDto>();

            for (int i = 1; i < 4; i++)
            {
                userDtos.Add(new ApplicationUserDto(i, $"TestUser{i}", false, i,
                    new DivisionDto(i, $"TestDivision{i}", default, i, default, default), new List<PhoneNumberDto>()
                    {
                        new(i, $"(1234)-567-89-1{i}", i, null)
                    }));
            }
            return userDtos;
        }

        public static CreateUserDto GetOneInvalidCreateDto()
        {
            return new CreateUserDto("CreatedUserDto1", false, int.MaxValue);
        }
        
        public static CreateUserDto GetOneCreateDto()
        {
            return new CreateUserDto("CreatedUserDto1", false, 1);
        }

        public static UpdateUserDto GetOneUpdateDto()
        {
            return new UpdateUserDto(2, "UpdatedUserDto1", false, 1);
        }

        public static UpdateUserDto GetUpdateDtoWithInvalidId()
        {
            return new UpdateUserDto(int.MaxValue, "InvalidIdUpdateDto", false, 1);
        }

        public static UpdateUserDto GetUpdateDtoWithInvalidDivisionId()
        {
            return new UpdateUserDto(1, "InvalidDivisionIdUpdateDto", false, Int32.MaxValue);
        }
    }
}