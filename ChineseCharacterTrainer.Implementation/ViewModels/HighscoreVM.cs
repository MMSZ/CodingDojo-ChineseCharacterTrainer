using System;
using System.Collections.Generic;
using System.Dynamic;
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

        public List<dynamic> Highscores { get; private set; }

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

            var highscores = _repository.GetAll<Highscore>();
            Highscores = GetBestAllTimeHighscores(highscores);
            BestHighscore = GetBestUserHighscores(highscores);
        }

        private Highscore GetBestUserHighscores(IEnumerable<Highscore> highscores)
        {
            // Note: Currently the user's name is used to identify the user. This is because every time a new user
            //       is created before the highscore is uploaded. To avoid this a user should have a login and
            //       always the same ID.
            return highscores.OrderBy(p => p.Score).First(p => p.User.Name == CurrentHighscore.User.Name);
        }

        private List<dynamic> GetBestAllTimeHighscores(IEnumerable<Highscore> highscores)
        {
            var i = 1;
            return highscores.OrderBy(p => p.Score)
                .Where(p => p.Dictionary.Id == CurrentHighscore.Dictionary.Id)
                .Take(5)
                .Select<Highscore, object>(
                    p =>
                        {
                            dynamic expandoObject = new ExpandoObject();
                            expandoObject.Ranking = i++;
                            expandoObject.Username = p.User.Name;
                            expandoObject.Score = p.Score;
                            return expandoObject;
                        }).ToList();
        }

        public event Action ReturnToMenuRequested;

        protected virtual void RaiseReturnToMenuRequested()
        {
            var handler = ReturnToMenuRequested;
            if (handler != null) handler();
        }
    }
}