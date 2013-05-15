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

        public void Initialize(List<DictionaryEntry> dictionaryEntries)
        {
            _dictionaryEntries = new Queue<DictionaryEntry>(dictionaryEntries);
            CurrentEntry = _dictionaryEntries.Dequeue();
        }

        public ICommand AnswerCommand { get { return _answerCommand ?? (_answerCommand = new RelayCommand(p => AnswerCurrentEntry())); }}

        public string Answer
        {
            get { return _answer; }
            set { _answer = value; RaisePropertyChanged(() => Answer); }
        }

        private void AnswerCurrentEntry()
        {
            CurrentEntry = _dictionaryEntries.Count > 0 ? _dictionaryEntries.Dequeue() : null;
            Answer = string.Empty;
        }

        public DictionaryEntry CurrentEntry
        {
            get { return _currentEntry; }
            private set { _currentEntry = value; RaisePropertyChanged(() => CurrentEntry);}
        }
    }
}
