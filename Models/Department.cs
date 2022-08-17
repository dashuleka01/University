using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("department")]
    public class Department
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("faculty_id")]
        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        [Column("address_id")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
