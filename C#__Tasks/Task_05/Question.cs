using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    internal abstract class Question
    {
        public int Marks { get; private set; }
        public string QuestionText { get; private set; }
        public bool IsAutoGraded { get; protected set; }

        protected Question(string questionText, int marks)
        {
            if (string.IsNullOrWhiteSpace(questionText))
                throw new ArgumentException("Question text cannot be empty.", nameof(questionText));
            if (marks < 0)
                throw new ArgumentException("Marks cannot be negative.", nameof(marks));
            QuestionText = questionText;
            Marks = marks;
            IsAutoGraded = true; // Default, overridden in EssayText
        }

        public abstract bool IsCorrectAnswer(string answer);
    }
}
