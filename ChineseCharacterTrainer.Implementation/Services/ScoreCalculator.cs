using System;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class ScoreCalculator : IScoreCalculator
    {
        public int CalculateScore(TimeSpan duration, int numberOfIncorrectAnswers)
        {
            return (int)duration.TotalSeconds + numberOfIncorrectAnswers * 5;
        }
    }
}