using System.ComponentModel.DataAnnotations;

namespace ShoppingWebAPI.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
