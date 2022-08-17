using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("faculty")]
    public class Faculty
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("address_id")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
