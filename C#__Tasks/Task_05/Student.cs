using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ExaminationSystem
{
    internal class Student
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        private readonly List<Course> _courses;
        private readonly Dictionary<Exam, int> _examScores;
        public IReadOnlyList<Course> Courses => _courses.AsReadOnly();
        public IReadOnlyDictionary<Exam, int> ExamScores => _examScores.AsReadOnly();

        public Student(string id, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID cannot be empty.", nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                throw new ArgumentException("Invalid email format.", nameof(email));

            ID = id;
            Name = name;
            Email = email;
            _courses = new List<Course>();
            _examScores = new Dictionary<Exam, int>();
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public void EnrollInCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));
            if (!_courses.Contains(course))
            {
                _courses.Add(course);
                course.EnrollStudent(this); // Maintain bidirectional relationship
            }
        }

        public void UnenrollFromCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));
            if (_courses.Remove(course))
            {
                course.UnenrollStudent(this); // Maintain bidirectional relationship
            }
        }

        public void RecordExamScore(Exam exam, List<string> answers)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));
            if (!_courses.Contains(exam.Course))
                throw new InvalidOperationException("Student is not enrolled in the course.");
            int score = exam.CalculateScore(answers);
            _examScores[exam] = score;
        }

        public string GenerateReport(Exam exam, int passingScore)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));
            if (!_examScores.ContainsKey(exam))
                throw new InvalidOperationException("No score recorded for this exam.");
            int score = _examScores[exam];
            bool passed = score >= passingScore;
            return $"Exam: {exam.Course.Title}\nStudent: {Name}\nCourse: {exam.Course.Title}\nScore: {score}\nStatus: {(passed ? "Pass" : "Fail")}";
        }

        public static int CompareScores(Student student1, Student student2, Exam exam)
        {
            if (student1 == null || student2 == null || exam == null)
                throw new ArgumentNullException("Arguments cannot be null.");
            if (!student1._examScores.ContainsKey(exam) || !student2._examScores.ContainsKey(exam))
                throw new InvalidOperationException("Both students must have scores for the exam.");
            return student1._examScores[exam].CompareTo(student2._examScores[exam]);
        }
    }
}