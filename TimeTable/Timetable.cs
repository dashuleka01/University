using DataAccess;
using Models;
using System.Collections.Generic;
using System.Linq;
using TimeTable.TimetableModels;

namespace TimeTable
{
    public class Timetable : ITimetable
    {
        public IReadOnlyList<TimetableCell> TimetableGroup(int groupId, IRepository<Lesson> lessonRepo, IRepository<Group> groupRepo)
        {
            var group = groupRepo.Get(groupId);
            if (group == null) return null;

            var lessongroup = group.LessonGroups;
            if (lessongroup == null) return null;

            var list = new List<TimetableCell>();
            foreach (var lessonId in lessongroup)
            {
                var lesson = lessonRepo.GetAll().First(l => l.Id == lessonId.LessonId);
                list.Add(new TimetableCell(lesson.EvenDateTime, lesson.Subject.Name,
                    lesson.TypeLesson.Name, lesson.Teacher.Surname, lesson.Teacher.Name, lesson.EvenAddress));
            }

            return list.OrderBy(x => x.DateTime).ToList();
        }

        public IReadOnlyList<TimetableCell> TimetableTeacher(int id, IRepository<Lesson> lessonRepo, IRepository<Teacher> teacherRepo)
        {
            var teacher = teacherRepo.Get(id);
            if (teacher == null) return null;

            var lessons = lessonRepo.GetAll().Where(l => l.TeacherId == id).ToList();
            var list = new List<TimetableCell>();
            foreach (var lesson in lessons)
                list.Add(new TimetableCell(lesson.EvenDateTime, lesson.Subject.Name, lesson.TypeLesson.Name,
                    lesson.Teacher.Surname, lesson.Teacher.Name, lesson.EvenAddress));
            return list;
        }
    }
}
