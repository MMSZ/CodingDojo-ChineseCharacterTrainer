using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.ServiceReference;
using ChineseCharacterTrainer.Library;
using System;
using System.Windows.Input;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class SummaryVM : ViewModel, ISummaryVM
    {
        private readonly IChineseCharacterTrainerService _chineseCharacterTrainerService;
        private string _username;
        private ICommand _uploadScoreCommand;

        private QuestionResult _questionResult;

        public SummaryVM(IChineseCharacterTrainerService chineseCharacterTrainerService)
        {
            _chineseCharacterTrainerService = chineseCharacterTrainerService;
        }

        public int NumberOfCorrectAnswers
        {
            get { return _questionResult.NumberOfCorrectAnswers; }
        }

        public int NumberOfIncorrectAnswers
        {
            get { return _questionResult.NumberOfIncorrectAnswers; }
        }

        public TimeSpan Duration
        {
            get { return _questionResult.Duration; }
        }

        public int Score
        {
            get { return _questionResult.Score; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged(() => Username); }
        }

        public ICommand UploadScoreCommand
        {
            get
            {
                return _uploadScoreCommand ??
                       (_uploadScoreCommand =
                       new RelayCommand(
                           p =>
                               {
                                   var user = new User();
                                   user.Name = Username;

                                   var highscore = new Highscore();
                                   highscore.Score = Score;
                                   highscore.User = user;

                                   _chineseCharacterTrainerService.UploadHighscore(highscore);
                                   RaiseUploadFinished(highscore);
                               }, p => !string.IsNullOrWhiteSpace(Username)));
            }
        }

        public void Initialize(QuestionResult questionResult)
        {
            _questionResult = questionResult;
        }

        public event Action<Highscore> UploadFinished;

        public void RaiseUploadFinished(Highscore highScore)
        {
            var handler = UploadFinished;
            if (handler != null) handler(highScore);
        }
    }
}
