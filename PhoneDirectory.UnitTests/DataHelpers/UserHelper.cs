using System.Collections.Generic;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.UnitTests.DataHelpers
{
    public static class UserHelper
    {
        public static ApplicationUser GetOne()
        {
            return new ApplicationUser()
            {
                Name = "TestUser1",
                DivisionId = 1,
                IsChief = false
            };
        }
    }
}