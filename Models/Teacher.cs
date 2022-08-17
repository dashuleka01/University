using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("teacher")]
    public class Teacher
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("department_id")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Column("position_id")]
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

    }
}
