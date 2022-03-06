using System.ComponentModel.DataAnnotations;

namespace PhoneDirectory.Domain.BaseEntities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}