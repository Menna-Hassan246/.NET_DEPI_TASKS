using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    internal class TrueFalse : Question
    {
        public bool CorrectAnswer { get; private set; }

        public TrueFalse(string questionText,int marks, bool correctAnswer)
            : base(questionText, marks)
        {
            CorrectAnswer = correctAnswer;
        }

        public override bool IsCorrectAnswer(string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
                return false;
            return bool.TryParse(answer.Trim(), out bool studentAnswer) && studentAnswer == CorrectAnswer;
        }
    }
}

