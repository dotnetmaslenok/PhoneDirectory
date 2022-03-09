using System.Collections.Generic;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.UnitTests.DataHelpers
{
    public static class UserHelper
    {
        public static ApplicationUser GetOneDefaultEntity()
        {
            return new ApplicationUser()
            {
                Name = "TestUser1",
                DivisionId = 1,
                IsChief = false
            };
        }

        public static ApplicationUser GetOneCreatedEntity()
        {
            return new ApplicationUser()
            {
                Name = "CreatedDivisionDto1",
                DivisionId = 1,
                IsChief = false
            };
        }

        public static IEnumerable<ApplicationUser> GetManyDefaultEntities()
        {
            return new List<ApplicationUser>()
            {
                new() {Name = "TestUser2", DivisionId = 3, IsChief = false},
                new() {Name = "TestUser3", DivisionId = 4, IsChief = false},
                new() {Name = "TestUser4", DivisionId = 5, IsChief = false},
                new() {Name = "TestUser5", DivisionId = 6, IsChief = false}
            };
        }
    }
}