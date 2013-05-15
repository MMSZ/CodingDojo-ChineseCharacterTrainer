using System.Collections.Generic;
using System.Windows.Input;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Library;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class QuestionVM : ViewModel, IQuestionVM
    {
        private ICommand _answerCommand;
        private DictionaryEntry _currentEntry;
        private Queue<DictionaryEntry> _dictionaryEntries;
        private string _answer;
        private bool _isInAnswerMode;
        private bool _lastAnswerWasCorrect;

        public void Initialize(List<DictionaryEntry> dictionaryEntries)
        {
            _dictionaryEntries = new Queue<DictionaryEntry>(dictionaryEntries);
            CurrentEntry = _dictionaryEntries.Dequeue();
            Answer = string.Empty;
            IsInAnswerMode = true;
        }

        public ICommand AnswerCommand { get { return _answerCommand ?? (_answerCommand = new RelayCommand(p => AnswerCurrentEntry())); }}

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

        private void AnswerCurrentEntry()
        {
            if (CurrentEntry == null)
            {
                return;
            }

            if (IsInAnswerMode)
            {
                LastAnswerWasCorrect = Answer == CurrentEntry.Pinyin;
            }
            else
            {
                CurrentEntry = _dictionaryEntries.Count > 0 ? _dictionaryEntries.Dequeue() : null;
                Answer = string.Empty;
            }
            
            IsInAnswerMode = !IsInAnswerMode;
        }

        public DictionaryEntry CurrentEntry
        {
            get { return _currentEntry; }
            private set { _currentEntry = value; RaisePropertyChanged(() => CurrentEntry);}
        }
    }
}
