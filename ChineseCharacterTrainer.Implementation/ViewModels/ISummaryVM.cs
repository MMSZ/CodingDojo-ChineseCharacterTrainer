using System.Windows.Input;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Library;
using System;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public interface ISummaryVM : IViewModel
    {
        int NumberOfCorrectAnswers { get; }
        int NumberOfIncorrectAnswers { get; }
        TimeSpan Duration { get; }
        int Score { get; }
        string Username { get; set; }

        ICommand UploadScoreCommand { get; }

        void Initialize(Dictionary dictionary, QuestionResult questionResult);

        event Action<Highscore> UploadFinished;
    }
}
