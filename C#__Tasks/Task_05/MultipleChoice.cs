using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    internal class MultipleChoice:Question
    {
        private readonly List<string> _options;
        public IReadOnlyList<string> Options => _options.AsReadOnly();
        public int CorrectAnswerIndex { get; private set; }

        public MultipleChoice(string questionText, int marks, List<string> options, int correctAnswerIndex)
            : base(questionText, marks)
        {
            if (options == null || options.Count < 2)
                throw new ArgumentException("Multiple choice questions must have at least two options.", nameof(options));
            if (correctAnswerIndex < 0 || correctAnswerIndex >= options.Count)
                throw new ArgumentException("Correct answer index is out of range.", nameof(correctAnswerIndex));
            _options = new List<string>(options);
            CorrectAnswerIndex = correctAnswerIndex;
        }

        public override bool IsCorrectAnswer(string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
                return false;
            // Allow answer as option text or index
            if (int.TryParse(answer, out int answerIndex))
                return answerIndex == CorrectAnswerIndex;
            return answer.Trim().ToLower() == _options[CorrectAnswerIndex].Trim().ToLower();
        }
    }
}

