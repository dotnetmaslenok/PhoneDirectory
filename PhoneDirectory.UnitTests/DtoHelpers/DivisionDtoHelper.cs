using System.Collections.Generic;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;

namespace PhoneDirectory.UnitTests.DtoHelpers
{
    public static class DivisionDtoHelper
    {
        public static DivisionDto GetOneDefaultDto()
        {
            return new DivisionDto(1, "TestDivision1", 
                new List<ApplicationUserDto>()
                {
                    new ApplicationUserDto(1, "TestUser1", false, 1, default,
                        new List<PhoneNumberDto>()
                        {
                            new(1, "(1234)-567-89-11", 1, default)
                        })
                },
                default, default, new List<DivisionDto>()
                {
                    new DivisionDto(2, "NestedDivision1", default, 1, default, default)
                });
        }

        public static List<DivisionDto> GetManyDefaultDtos()
        {
            var divisionDtos = new List<DivisionDto>();

            for (int i = 2; i < 5; i++)
            {
                divisionDtos.Add(new DivisionDto(i, $"TestDivision{i}", 
                    new List<ApplicationUserDto>()
                    {
                        new ApplicationUserDto(i, $"TestUser{i}", false, i, default,
                            new List<PhoneNumberDto>()
                            {
                                new(i, $"(1234)-567-89-1{i}", i, default)
                            })
                    },
                    default, default, new List<DivisionDto>()
                    {
                        new DivisionDto(i, $"NestedDivision{i}", default, i, default, default)
                    }));
            }

            return divisionDtos;
        }

        public static CreateDivisionDto GetOneCreateDto()
        {
            return new CreateDivisionDto("CreatedDivisionDto1",
                null,
                new List<CreateDivisionDto>()
                {
                    new("CreatedNestedDivisionDto1", default, default)
                });
        }

        public static UpdateDivisionDto GetOneUpdateDto()
        {
            return new UpdateDivisionDto(2, "UpdatedDivisionName");
        }

        public static UpdateDivisionDto GetOneInvalidUpdateDto()
        {
            return new UpdateDivisionDto(int.MaxValue, "InvalidUpdateDto");
        }
    }
}