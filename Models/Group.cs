using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("group")]
    public class Group
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("department_id")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<LessonGroup> LessonGroups { get; set; } 
    }
}
