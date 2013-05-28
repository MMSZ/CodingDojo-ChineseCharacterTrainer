using System;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IScoreCalculator
    {
        int CalculateScore(TimeSpan duration, int numberOfIncorrectAnswers);
    }
}