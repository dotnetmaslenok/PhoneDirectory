using System.Collections.Generic;
using System.Net.NetworkInformation;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.UnitTests.DtoHelpers
{
    public static class DivisionDtoHelper
    {
        public static DivisionDto GetOneDefaultDto()
        {
            return new DivisionDto(1, "TestDivision1", 
                new List<ApplicationUserDto>()
                {
                    new ApplicationUserDto(1, "TestUser1", false, 1, null,
                        new List<PhoneNumberDto>()
                        {
                            new(1, "(1234)-567-89-11", 1, null)
                        })
                },
                new List<DivisionDto>()
                {
                    new DivisionDto(2, "NestedDivision1", null, null)
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
                        new ApplicationUserDto(i, $"TestUser{i}", false, i, null,
                            new List<PhoneNumberDto>()
                            {
                                new(i, $"(1234)-567-89-1{i}", i, null)
                            })
                    },
                    new List<DivisionDto>()
                    {
                        new DivisionDto(i, $"NestedDivision{i}", null, null)
                    }));
            }

            return divisionDtos;
        }

        public static CreateDivisionDto GetOneCreateDto()
        {
            return new CreateDivisionDto("CreatedDivisionDto1",
                new List<CreateDivisionDto>()
                {
                    new("CreatedNestedDivisionDto1", null)
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