using System;
using System.Collections.Generic;

namespace ExaminationSystem
{
    internal class Exam
    {
        private List<Question> _questions = new List<Question>();
        public Course Course { get; private set; }
        public bool IsStarted { get; private set; }
        public IReadOnlyList<Question> Questions => _questions.AsReadOnly();

        public Exam(Course course)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
        }

        public void AddQuestion(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));
            if (IsStarted)
                throw new InvalidOperationException("Cannot modify exam after it has started.");

            int totalMarks = 0;
            foreach (Question q in _questions)
            {
                totalMarks += q.Marks;
            }
            totalMarks += question.Marks;

            if (totalMarks <= Course.MaximumDegree)
                _questions.Add(question);
            else
                throw new InvalidOperationException("Total marks exceed course's maximum degree.");
        }

        public void EditQuestion(int index, Question updatedQuestion)
        {
            if (updatedQuestion == null)
                throw new ArgumentNullException(nameof(updatedQuestion));
            if (IsStarted)
                throw new InvalidOperationException("Cannot modify exam after it has started.");
            if (index < 0 || index >= _questions.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            int totalMarks = 0;
            for (int i = 0; i < _questions.Count; i++)
            {
                if (i == index)
                    continue; 
                totalMarks += _questions[i].Marks;
            }
            totalMarks += updatedQuestion.Marks;

            if (totalMarks <= Course.MaximumDegree)
                _questions[index] = updatedQuestion;
            else
                throw new InvalidOperationException("Total marks exceed course's maximum degree.");
        }

        public void RemoveQuestion(int index)
        {
            if (IsStarted)
                throw new InvalidOperationException("Cannot modify exam after it has started.");
            if (index < 0 || index >= _questions.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            _questions.RemoveAt(index);
        }

        public void StartExam()
        {
            IsStarted = true;
        }

        public Exam DuplicateForCourse(Course newCourse)
        {
            var duplicatedExam = new Exam(newCourse)
            {
                _questions = new List<Question>(_questions) 
            };
            return duplicatedExam;
        }

        public int CalculateScore(List<string> studentAnswers)
        {
            if (studentAnswers == null)
                throw new ArgumentNullException(nameof(studentAnswers));
            if (studentAnswers.Count != _questions.Count)
                throw new ArgumentException("The number of answers must match the number of questions.");

            int score = 0;
            for (int i = 0; i < _questions.Count; i++)
            {
                if (_questions[i].IsAutoGraded && _questions[i].IsCorrectAnswer(studentAnswers[i]))
                    score += _questions[i].Marks;
            }
            return score;
        }
    }

    }