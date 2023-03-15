using System.ComponentModel.DataAnnotations;

namespace ShoppingWebAPI.DAL.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
