using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("lesson_group")]
    public class LessonGroup
    {

        [Column("lesson_id")]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }


        [Column("group_id")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
