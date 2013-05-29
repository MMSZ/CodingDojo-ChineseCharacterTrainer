using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Library;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class HighscoreVM : ViewModel, IHighscoreVM
    {
        private readonly IRepository _repository;
        private ICommand _continueCommand;

        public List<Highscore> Highscores { get; private set; }

        public Highscore CurrentHighscore { get; private set; }

        public Highscore BestHighscore { get; private set; }

        public HighscoreVM(IRepository repository)
        {
            _repository = repository;
        }

        public ICommand ContinueCommand
        {
            get { return _continueCommand ?? (_continueCommand = new RelayCommand(p => RaiseReturnToMenuRequested())); }
        }

        public void Initialize(Highscore currentHighscore)
        {
            CurrentHighscore = currentHighscore;

            var highscores = _repository.GetHighscores();
            Highscores = GetBestAllTimeHighscores(highscores);
            BestHighscore = GetBestUserHighscores(highscores);
        }

        private Highscore GetBestUserHighscores(IEnumerable<Highscore> highscores)
        {
            return highscores.OrderBy(p => p.Score).First(p => p.User.Id == CurrentHighscore.User.Id);
        }

        private List<Highscore> GetBestAllTimeHighscores(IEnumerable<Highscore> highscores)
        {
            return highscores.OrderBy(p => p.Score)
                             .Where(p => p.Dictionary.Id == CurrentHighscore.Dictionary.Id)
                             .Take(5)
                             .ToList();
        }

        public event Action ReturnToMenuRequested;

        protected virtual void RaiseReturnToMenuRequested()
        {
            var handler = ReturnToMenuRequested;
            if (handler != null) handler();
        }
    }
}