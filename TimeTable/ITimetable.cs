using DataAccess;
using Models;
using System.Collections.Generic;
using TimeTable.TimetableModels;

namespace TimeTable
{
    public interface ITimetable
    {
        IReadOnlyList<TimetableCell> TimetableGroup(int groupId, IRepository<Lesson> lessonRepo, IRepository<Group> groupRepo);
        IReadOnlyList<TimetableCell> TimetableTeacher(int id, IRepository<Lesson> lessonRepo, IRepository<Teacher> teacherRepo);
    }
}