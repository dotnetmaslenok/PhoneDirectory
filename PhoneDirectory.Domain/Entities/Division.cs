using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhoneDirectory.Domain.BaseEntities;

namespace PhoneDirectory.Domain.Entities
{
    public class Division : BaseEntity
    {
        public string Name { get; set; }
        
        public List<ApplicationUser> Users { get; set; }
        
        public List<Division> Divisions { get; set; }

        public Division()
        {
            Divisions = new List<Division>();
        }
    }
}