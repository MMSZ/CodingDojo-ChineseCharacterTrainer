using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Library;
using System;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public interface ISummaryVM : IViewModel
    {
        int NumberOfCorrectAnswers { get; }
        int NumberOfIncorrectAnswers { get; }
        TimeSpan Duration { get; }

        void Initialize(QuestionResult questionResult);
    }
}
