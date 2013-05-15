using System;

namespace ChineseCharacterTrainer.Implementation.Model
{
    public class QuestionResult
    {
        public QuestionResult(int numberOfCorrectAnswers, int numberOfIncorrectAnswers, TimeSpan duration)
        {
            NumberOfCorrectAnswers = numberOfCorrectAnswers;
            NumberOfIncorrectAnswers = numberOfIncorrectAnswers;
            Duration = duration;
        }

        public int NumberOfCorrectAnswers { get; private set; }
        public int NumberOfIncorrectAnswers { get; private set; }
        public TimeSpan Duration { get; private set; }
    }
}
