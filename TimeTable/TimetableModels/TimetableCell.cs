using Models;
using System;

namespace TimeTable.TimetableModels
{
    public class TimetableCell
    {
        public TimetableCell(DateTime dateTime, string subject, string type, string teacherSurname, string teacherName, Address address)
        {
            DateTime = dateTime;
            Subject = subject;
            Type = type;
            TeacherSurname = teacherSurname;
            TeacherName = teacherName;
            Address = address;
        }

        public DateTime DateTime { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string TeacherSurname { get; set; }
        public string TeacherName { get; set; }
        public Address Address { get; set; }
    }
}
