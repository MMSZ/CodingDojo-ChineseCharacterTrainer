using System;
using System.Collections.Generic;
using System.Windows.Input;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.Utilities;
using ChineseCharacterTrainer.Library;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class QuestionVM : ViewModel, IQuestionVM
    {
        private readonly IDateTime _dateTime;
        private ICommand _answerCommand;
        private DictionaryEntry _currentEntry;
        private Queue<DictionaryEntry> _dictionaryEntries;
        private string _answer;
        private bool _isInAnswerMode;
        private bool _lastAnswerWasCorrect;
        private int _numberOfCorrectAnswers;
        private int _numberOfIncorrectAnswers;
        private DateTime _startTime;

        public QuestionVM(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public void Initialize(List<DictionaryEntry> dictionaryEntries)
        {
            _dictionaryEntries = new Queue<DictionaryEntry>(dictionaryEntries);
            CurrentEntry = _dictionaryEntries.Dequeue();
            Answer = string.Empty;
            IsInAnswerMode = true;
            NumberOfCorrectAnswers = 0;
            NumberOfIncorrectAnswers = 0;
            _startTime = _dateTime.Now;
        }

        public event Action<QuestionResult> QuestionsFinished;

        private void RaiseQuestionsFinished(QuestionResult result)
        {
            var handler = QuestionsFinished;
            if (handler != null) handler(result);
        }

        public ICommand AnswerCommand
        {
            get { return _answerCommand ?? (_answerCommand = new RelayCommand(p => AnswerCurrentEntry())); }
        }

        public bool IsInAnswerMode
        {
            get { return _isInAnswerMode; }
            private set { _isInAnswerMode = value; RaisePropertyChanged(() => IsInAnswerMode);}
        }

        public bool LastAnswerWasCorrect
        {
            get { return _lastAnswerWasCorrect; }
            private set { _lastAnswerWasCorrect = value; RaisePropertyChanged(() => LastAnswerWasCorrect); }
        }

        public string Answer
        {
            get { return _answer; }
            set { _answer = value; RaisePropertyChanged(() => Answer); }
        }

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

        public DictionaryEntry CurrentEntry
        {
            get { return _currentEntry; }
            private set { _currentEntry = value; RaisePropertyChanged(() => CurrentEntry);}
        }

        private void AnswerCurrentEntry()
        {
            if (CurrentEntry == null)
            {
                return;
            }

            if (IsInAnswerMode)
            {
                LastAnswerWasCorrect = Answer == CurrentEntry.Pinyin;
                
                if (LastAnswerWasCorrect) NumberOfCorrectAnswers++;
                else NumberOfIncorrectAnswers++;
            }
            else
            {
                if (_dictionaryEntries.Count == 0)
                {
                    CurrentEntry = null;
                    RaiseQuestionsFinished(
                        new QuestionResult(NumberOfCorrectAnswers, _numberOfIncorrectAnswers, _dateTime.Now - _startTime));
                    return;
                }

                CurrentEntry = _dictionaryEntries.Dequeue();
                Answer = string.Empty;
            }
            
            IsInAnswerMode = !IsInAnswerMode;
        }
    }
}
