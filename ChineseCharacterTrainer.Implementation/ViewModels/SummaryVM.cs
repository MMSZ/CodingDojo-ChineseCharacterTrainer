using System;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Library;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class SummaryVM : ViewModel, ISummaryVM
    {
        private int _numberOfCorrectAnswers;
        private int _numberOfIncorrectAnswers;
        private TimeSpan _duration;

        public int NumberOfCorrectAnswers
        {
            get { return _numberOfCorrectAnswers; }
            private set { _numberOfCorrectAnswers = value; RaisePropertyChanged(() => NumberOfCorrectAnswers); }
        }

        public int NumberOfIncorrectAnswers
        {
            get { return _numberOfIncorrectAnswers; }
            private set { _numberOfIncorrectAnswers = value; RaisePropertyChanged(() => NumberOfIncorrectAnswers); }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            private set { _duration = value; RaisePropertyChanged(() => Duration); }
        }

        public void Initialize(QuestionResult questionResult)
        {
            NumberOfCorrectAnswers = questionResult.NumberOfCorrectAnswers;
            NumberOfIncorrectAnswers = questionResult.NumberOfIncorrectAnswers;
            Duration = questionResult.Duration;
        }
    }
}
