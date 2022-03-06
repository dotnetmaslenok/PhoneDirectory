using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhoneDirectory.Domain.BaseEntities;

namespace PhoneDirectory.Domain.Entities
{
    public class PhoneNumber : BaseEntity
    {
        public string UserPhoneNumber { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public ApplicationUser User { get; set; }
    }
}