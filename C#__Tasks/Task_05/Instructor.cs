using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    internal class Instructor
    {

        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Specialization { get; private set; }
        private readonly List<Course> _courses;
        public IReadOnlyList<Course> Courses => _courses.AsReadOnly();

        public Instructor(string id, string name, string specialization)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(specialization))
                throw new ArgumentException("Specialization cannot be empty.", nameof(specialization));

            ID = id;
            Name = name;
            Specialization = specialization;
            _courses = new List<Course>();
        }

        public void AddCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));
            if (!_courses.Contains(course))
            {
                _courses.Add(course);
            }
        }

        public void RemoveCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));
            _courses.Remove(course);
        }

        public Exam DuplicateExam(Exam sourceExam, Course targetCourse)
        {
            if (sourceExam == null)
                throw new ArgumentNullException(nameof(sourceExam));
            if (targetCourse == null)
                throw new ArgumentNullException(nameof(targetCourse));
            if (!_courses.Contains(targetCourse))
                throw new InvalidOperationException("Instructor is not assigned to the target course.");
            Exam duplicatedExam = sourceExam.DuplicateForCourse(targetCourse);
            targetCourse.AddExam(duplicatedExam);
            return duplicatedExam;
        }
    }
}
