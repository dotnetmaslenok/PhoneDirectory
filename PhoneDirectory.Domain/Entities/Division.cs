using System.Collections.Generic;
using PhoneDirectory.Domain.BaseEntities;

namespace PhoneDirectory.Domain.Entities
{
    public class Division : BaseEntity
    {
        public string Name { get; set; }
        
        public List<ApplicationUser> Users { get; set; }

        public Division ParentDivision { get; set; }
        
        public List<Division> ChildDivisions { get; set; }

        public Division()
        {
            ChildDivisions = new List<Division>();
        }
    }
}