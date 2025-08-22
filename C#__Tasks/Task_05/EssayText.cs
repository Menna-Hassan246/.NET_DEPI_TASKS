using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    internal class EssayText : Question
    {
        public string ModelAnswer { get; private set; }

        public EssayText(string questionText, int marks, string modelAnswer)
            : base(questionText, marks)
        {
            ModelAnswer = modelAnswer ?? string.Empty;
            IsAutoGraded = false; // Essay questions are not auto-graded
        }

        public override bool IsCorrectAnswer(string answer)
        {
            throw new InvalidOperationException("Essay questions cannot be auto-graded.");
        }
    }
}