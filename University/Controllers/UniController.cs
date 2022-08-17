using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeTable;
using TimeTable.TimetableModels;

namespace University.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniController : ControllerBase
    {
        private readonly IRepository<Student> _studentRepo;
        private readonly IRepository<Group> _groupRepo;
        private readonly IRepository<Lesson> _lessonRepo;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<Department> _departRepo;
        private readonly IRepository<LessonGroup> _lessonGroupRepo;
        private readonly ITimetable _timeTable;

        public UniController(IRepository<Student> studentRepo, IRepository<Group> groupRepo, IRepository<Lesson> lessonRepo, ITimetable timeTable, IRepository<Teacher> teacherRepo, IRepository<Department> departRepo, IRepository<LessonGroup> lessonGroupRepo)
        {
            _studentRepo = studentRepo;
            _groupRepo = groupRepo;
            _lessonRepo = lessonRepo;
            _timeTable = timeTable;
            _teacherRepo = teacherRepo;
            _departRepo = departRepo;
            _lessonGroupRepo = lessonGroupRepo;
        }


        [HttpGet]
        [Route("timetable/group/{id}")]
        public IEnumerable<TimetableCell> TimetableByGroup(int id)
        {
            return _timeTable.TimetableGroup(id, _lessonRepo, _groupRepo);
        }

        [HttpGet]
        [Route("timetable/teacher/{id}")]
        public IEnumerable<TimetableCell> TimetableByteacher(int id)
        {
            return _timeTable.TimetableTeacher(id, _lessonRepo, _teacherRepo);
        }

        [HttpGet]
        [Route("faculty/{id}/teachers")]
        public IEnumerable<Teacher> TeachersFromFaculty(int id)
        {
            var departs = _departRepo.GetAll().Where(d => d.FacultyId == id).Select(x => x.Id).ToList();
            return _teacherRepo.GetAll().ToList().Where(t => departs.Contains(t.DepartmentId));
        }

        [HttpGet]
        [Route("department/{id}/teachers")]
        public IEnumerable<Teacher> TeachersFromDepartment(int id)
        {
            return _teacherRepo.GetAll().ToList().Where(t => t.DepartmentId == id);
        }

        [HttpGet]
        [Route("faculty/{id}/departments")]
        public IEnumerable<Department> DepartmentsFromFaculty(int id)
        {
            return _departRepo.GetAll().ToList().Where(d => d.FacultyId == id);
        }


        [HttpGet]
        [Route("student/{id}")]
        public Dictionary<string, object> StudentInfo(int id)
        {
            var dict = new Dictionary<string, object>();
            var student = _studentRepo.Get(id);

            dict.Add("id", student.Id);
            dict.Add("name", student.Name);
            dict.Add("surname", student.Surname);
            dict.Add("group", student.GroupId);
            dict.Add("department", student.Group.Department.Name);
            dict.Add("faculty", student.Group.Department.Faculty.Name);
            dict.Add("phone", student.Phone);
            return dict;
        }

        [HttpGet]
        [Route("group/{id}")]
        public Dictionary<string, object> GroupInfo(int id)
        {
            var dict = new Dictionary<string, object>();
            var group = _groupRepo.Get(id);
            var students = group.Students.ToDictionary(st => st.Id,
                st => new Dictionary<string, string> { { "Surname", st.Surname }, { "Name", st.Name } });

            dict.Add("id", group.Id);
            dict.Add("department", group.Department.Name);
            dict.Add("faculty", group.Department.Faculty.Name);
            dict.Add("students", students);
            return dict;
        }

        [HttpPost]
        [Route("timetable/lesson")]
        public bool PostLesson(int subject, int type, int teacher, int even_addr, DateTime evenDatetime, int odd_addr, DateTime oddDatetime)
        {
            var lessons = _lessonRepo.GetAll();
            var lesson = new Lesson
            {
                Id = lessons.Last().Id + 1,
                TeacherId = teacher,
                SubjectId = subject,
                TypeLessonId = type,
                EvenAddressId = even_addr,
                EvenDateTime = evenDatetime,
                OddAddressId = odd_addr,
                OddDateTime = oddDatetime
            };

            if (lessons.Contains(lesson)) return false;
            _lessonRepo.New(lesson);
            return true;
        }

        [HttpPut]
        [Route("timetable/lesson/group")]
        public bool AddGroupsToLesson(IList<int> groupIds, int lessonId)
        {
            var lesson = _lessonRepo.Get(lessonId);
            var lessonGroup = new List<LessonGroup>();

            if (lesson == null) return false;
            foreach (var id in groupIds)
            {
                var group = _groupRepo.Get(id);
                if (group == null) return false;
            }

            foreach (var id in groupIds)
            {
                var timetable = _timeTable.TimetableGroup(id, _lessonRepo, _groupRepo);
                if (timetable.Where(t => t.DateTime == lesson.EvenDateTime || t.DateTime == lesson.OddDateTime).Any())
                    return false;
                lessonGroup.Add(new LessonGroup { GroupId = id, LessonId = lesson.Id });
            }

            foreach (var l in lessonGroup)
                _lessonGroupRepo.New(l);
            return true;
        }

        [HttpGet]
        [Route("students")]
        public IReadOnlyList<object> GetAllStudents(int count, int pageNum)
        {
            var list = new List<object>();
            var students = _studentRepo.GetAll();
            var minId = students.First().Id;
            var maxId = students.Last().Id;

            for (int i = minId + count * (pageNum - 1); i < minId + count * (pageNum - 1) + count; i++)
            {
                var student = students.Where(st => st.Id == i).First();
                Dictionary<string, object> dict = new()
                {
                    { "id", student.Id },
                    { "name", student.Name },
                    { "surname", student.Surname },
                    { "group", student.GroupId },
                    { "department", student.Group.Department.Name },
                    { "faculty", student.Group.Department.Faculty.Name },
                    { "phone", student.Phone }
                };
                list.Add(dict);
            }
            return list;
        }
    }
}
