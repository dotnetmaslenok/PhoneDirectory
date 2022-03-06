using System.Collections;
using System.Collections.Generic;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.UnitTests.DataHelpers
{
    public static class DivisionHelper
    {
        public static Division GetOne()
        {
            return new Division
            {
                Name = "TestDivision1", Divisions = new List<Division>()
                {
                    new Division() {Name = "NestedDivision1" }
                }
            };
        }
    }
}