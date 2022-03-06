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

            // new List<ApplicationUserDto>()
            // {
            //     new(1, "TestUser1", false, 1, 
            //         new DivisionDto(1, "TestDivision", null, null), 
            //         new List<PhoneNumberDto>()
            //         {
            //             new(1, "TestPhoneNumber1", 1, 
            //                 new ApplicationUserDto(1, "TestUser1", false, 1, null, null))
            //         })
            // },
            // new List<DivisionDto>()
            // {
            //     new(2, "NestedDivision1", null, null)
            // });
        }
        
        public static CreateDivisionDto GetOneCreateDto()
        {
            return new CreateDivisionDto("DivisionDto1",
                new List<CreateDivisionDto>()
                {
                    new("NestedDivisionDto1", null)
                });
        }

        public static UpdateDivisionDto GetOneUpdateDto()
        {
            return new UpdateDivisionDto(1, "UpdatedDivisionName");
        }

        public static UpdateDivisionDto GetOneInvalidUpdateDto()
        {
            return new UpdateDivisionDto(int.MaxValue, "InvalidUpdateDto");
        }
    }
}