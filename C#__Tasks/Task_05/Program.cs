using System;
using System.Collections.Generic;

namespace ExaminationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test 1: Course and Instructor Creation
            Console.WriteLine("Test 1: Course and Instructor Creation");
            var instructor = new Instructor("I001", "Dr. Smith", "Math");
            var course = new Course("Mathematics", "Basic Math Course", 100, instructor);
            Console.WriteLine($"Course Title: {course.Title}, Max Degree: {course.MaximumDegree}, Instructor: {instructor.Name}");
            Console.WriteLine($"Instructor Courses Count: {instructor.Courses.Count}");
            Console.WriteLine();

            // Test 2: Student Enrollment
            Console.WriteLine("Test 2: Student Enrollment");
            var student = new Student("S001", "menna hassan", "menna@example.com");
            course.EnrollStudent(student);
            Console.WriteLine($"Student Courses Count: {student.Courses.Count}");
            Console.WriteLine($"Course Students Count: {course.Students.Count}");
            Console.WriteLine();

            // Test 3: Exam Creation and Question Management
            Console.WriteLine("Test 3: Exam Creation and Question Management");
            var exam = new Exam(course);
            var mc = new MultipleChoice("What is 2+2?", 50, new List<string> { "2", "4", "6" }, 1);
            var tf = new TrueFalse("Is the sky blue?", 30, true);
            var essay = new EssayText("Describe the water cycle.", 20, "Water cycle description");
            exam.AddQuestion(mc);
            exam.AddQuestion(tf);
            exam.AddQuestion(essay);
            Console.WriteLine($"Exam Questions Count: {exam.Questions.Count}");
            exam.StartExam();
            try
            {
                exam.AddQuestion(new MultipleChoice("Extra", 10, new List<string> { "A", "B" }, 0));
                Console.WriteLine("Failed: Should not allow adding question after start.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Success: Prevented adding question after exam start.");
            }
            Console.WriteLine();

            // Test 4: Score Calculation
            Console.WriteLine("Test 4: Score Calculation");
            var answers = new List<string> { "4", "True", "Partial answer" };
            int score = exam.CalculateScore(answers);
            Console.WriteLine($"Calculated Score: {score} (Expected 80 for correct MC and TF)");
            Console.WriteLine();

            // Test 5: Record Exam Score and Generate Report
            Console.WriteLine("Test 5: Record Exam Score and Generate Report");
            student.RecordExamScore(exam, answers);
            string report = student.GenerateReport(exam, 60);
            Console.WriteLine("Report:");
            Console.WriteLine(report);
            Console.WriteLine();

            // Test 6: Compare Scores
            Console.WriteLine("Test 6: Compare Scores");
            var student2 = new Student("S002", "Jane Doe", "jane@example.com");
            course.EnrollStudent(student2);
            student2.RecordExamScore(exam, new List<string> { "2", "False", "Wrong answer" }); // Score: 0
            int comparison = Student.CompareScores(student, student2, exam);
            Console.WriteLine($"Comparison Result: {comparison} (Expected positive, student1 > student2)");
            Console.WriteLine();

            // Test 7: Exam Duplication
            Console.WriteLine("Test 7: Exam Duplication");
            var newCourse = new Course("Algebra", "Basic Algebra", 100, instructor);
            var duplicatedExam = instructor.DuplicateExam(exam, newCourse);
            Console.WriteLine($"Duplicated Exam Questions Count: {duplicatedExam.Questions.Count}");
            Console.WriteLine($"New Course Exams Count: {newCourse.Exams.Count}");
        }
    }
}