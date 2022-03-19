using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PhoneDirectory.Domain.BaseEntities;

namespace PhoneDirectory.Domain.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public string Name { get; set; }
        public bool IsChief { get; set; }
        
        [ForeignKey("Division")]
        public int DivisionId { get; set; }
        
        public Division Division { get; set; }
        
        public List<PhoneNumber> PhoneNumbers { get; set; }

        public ApplicationUser()
        {
            PhoneNumbers = new List<PhoneNumber>();
        }
    }
}