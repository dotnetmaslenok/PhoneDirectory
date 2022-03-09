using System.Collections.Generic;
using PhoneDirectory.Application.Dtos.CreateDtos;
using PhoneDirectory.Application.Dtos.GetDtos;
using PhoneDirectory.Application.Dtos.UpdateDtos;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.UnitTests.DtoHelpers
{
    public static class PhoneNumberDtoHelper
    {
        public static PhoneNumberDto GetOne()
        {
            return new PhoneNumberDto(1, "(1234)-567-89-11", 1,
                UserDtoHelper.GetOneDefaultDto());
        }

        public static CreatePhoneNumberDto GetOneInvalidCreateDto()
        {
            return new CreatePhoneNumberDto("(1432)-312-21-21", int.MaxValue);
        }

        public static CreatePhoneNumberDto GetOneCreateDto()
        {
            return new CreatePhoneNumberDto("(9876)-543-21-00", 1);
        }
        
        public static UpdatePhoneNumberDto GetOneUpdateDto()
        {
            return new UpdatePhoneNumberDto(1, "(5432)-678-90-01", 1);
        }

        public static UpdatePhoneNumberDto GetOneUpdateDtoWithInvalidId()
        {
            return new UpdatePhoneNumberDto(int.MaxValue, "(5432)-678-90-01", 1);
        }

        public static UpdatePhoneNumberDto GetOneUpdateDtoWithInvalidUserId()
        {
            return new UpdatePhoneNumberDto(1, "(5432)-678-90-01", int.MaxValue);
        }
    }
}