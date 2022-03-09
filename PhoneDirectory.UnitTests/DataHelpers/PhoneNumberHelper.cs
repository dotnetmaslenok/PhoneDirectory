using System.Collections.Generic;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.UnitTests.DataHelpers
{
    public static class PhoneNumberHelper
    {
        public static PhoneNumber GetOneDefaultEntity()
        {
            return new PhoneNumber()
            {
                UserPhoneNumber = "(1234)-567-89-11",
                UserId = 1
            };
        }

        public static PhoneNumber GetOneCreatedEntity()
        {
            return new PhoneNumber()
            {
                UserPhoneNumber = "(9876)-543-21-00",
                UserId = 1
            };
        }

        public static IEnumerable<PhoneNumber> GetManyDefaultEntities()
        {
            return new List<PhoneNumber>()
            {
                new() {UserPhoneNumber = "(1234)-567-89-12", UserId = 2},
                new() {UserPhoneNumber = "(1234)-567-89-13", UserId = 3},
                new() {UserPhoneNumber = "(1234)-567-89-14", UserId = 4},
                new() {UserPhoneNumber = "(1234)-567-89-15", UserId = 5},
            };
        }
    }
}