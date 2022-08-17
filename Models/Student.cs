using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("student")]
    public class Student
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        [Column("phone")]
        public string Phone { get; set; }
    }
}
