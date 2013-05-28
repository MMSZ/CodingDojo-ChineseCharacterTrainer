using System;

namespace ChineseCharacterTrainer.Implementation.Model
{
    public class QuestionResult
    {
        public QuestionResult(int numberOfCorrectAnswers, int numberOfIncorrectAnswers, TimeSpan duration, int score)
        {
            NumberOfCorrectAnswers = numberOfCorrectAnswers;
            NumberOfIncorrectAnswers = numberOfIncorrectAnswers;
            Duration = duration;
            Score = score;
        }

        public int NumberOfCorrectAnswers { get; private set; }
        public int NumberOfIncorrectAnswers { get; private set; }
        public TimeSpan Duration { get; private set; }

        public int Score { get; private set; }
    }
}
