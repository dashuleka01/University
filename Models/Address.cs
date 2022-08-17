using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("address")]
    public class Address
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("street")]
        public string Street { get; set; }
        [Column("corpus")]
        public int? Corpus { get; set; }
        [Column("house")]
        public int House { get; set; }
        [Column("room")]
        public int Room { get; set; }
    }
}
