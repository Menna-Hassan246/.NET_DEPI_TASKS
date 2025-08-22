using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    internal class Course
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int MaximumDegree { get; private set; }
        private readonly List<Exam> _exams;
        private readonly List<Student> _students;
        public Instructor Instructor { get; private set; }
        public IReadOnlyList<Exam> Exams => _exams.AsReadOnly();
        public IReadOnlyList<Student> Students => _students.AsReadOnly();

        public Course(string title, string description, int maximumDegree, Instructor instructor)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.", nameof(description));
            if (maximumDegree <= 0)
                throw new ArgumentException("Maximum degree must be positive.", nameof(maximumDegree));
            if (instructor == null)
                throw new ArgumentNullException(nameof(instructor));

            Title = title;
            Description = description;
            MaximumDegree = maximumDegree;
            Instructor = instructor;
            _exams = new List<Exam>();
            _students = new List<Student>();
            instructor.AddCourse(this);
        }

        public void AddExam(Exam exam)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));
            if (exam.Course != this)
                throw new ArgumentException("Exam must belong to this course.", nameof(exam));
            _exams.Add(exam);
        }

        public void RemoveExam(int index)
        {
            if (index < 0 || index >= _exams.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (_exams[index].IsStarted)
                throw new InvalidOperationException("Cannot remove an exam that has started.");
            _exams.RemoveAt(index);
        }

        public void EnrollStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (!_students.Contains(student))
            {
                _students.Add(student);
                student.EnrollInCourse(this);
            }
        }

        public void UnenrollStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (_students.Remove(student))
            {
                student.UnenrollFromCourse(this); 
            }
        }
    }
}
