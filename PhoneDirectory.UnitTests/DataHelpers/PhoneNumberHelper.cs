using System.Collections.Generic;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.UnitTests.DataHelpers
{
    public static class PhoneNumberHelper
    {
        public static PhoneNumber GetOne()
        {
            return new PhoneNumber()
            {
                UserPhoneNumber = "(1234)-567-89-11",
                UserId = 1
            };
        }
    }
}