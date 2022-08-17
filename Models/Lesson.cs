using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("lesson")]
    public class Lesson
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("subject_id")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        [Column("type_lesson_id")]
        public int TypeLessonId { get; set; }
        public virtual TypeLesson TypeLesson { get; set; }

        [Column("teacher_id")]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [Column("even_datetime")]
        public DateTime EvenDateTime { get; set; }

        [Column("even_address_id")]
        public int EvenAddressId { get; set; }
        public virtual Address EvenAddress { get; set; }

        [Column("odd_datetime")]
        public DateTime OddDateTime { get; set; }

        [Column("odd_address_id")]
        public int OddAddressId { get; set; }
        public virtual Address OddAddress { get; set; }

        public virtual ICollection<LessonGroup> LessonGroups { get; set; }


        public override bool Equals(object obj)
        {
            return obj is Lesson lesson &&
                   SubjectId == lesson.SubjectId &&
                   TypeLessonId == lesson.TypeLessonId &&
                   TeacherId == lesson.TeacherId &&
                   EvenDateTime == lesson.EvenDateTime &&
                   EvenAddressId == lesson.EvenAddressId &&
                   OddDateTime == lesson.OddDateTime &&
                   OddAddressId == lesson.OddAddressId;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(SubjectId);
            hash.Add(Subject);
            hash.Add(TypeLessonId);
            hash.Add(TypeLesson);
            hash.Add(TeacherId);
            hash.Add(Teacher);
            hash.Add(EvenDateTime);
            hash.Add(EvenAddressId);
            hash.Add(EvenAddress);
            hash.Add(OddDateTime);
            hash.Add(OddAddressId);
            hash.Add(OddAddress);
            hash.Add(LessonGroups);
            return hash.ToHashCode();
        }
    }
}
