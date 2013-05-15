using System.Collections.Generic;
using System.Windows.Input;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Library;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public interface IQuestionVM : IViewModel
    {
        ICommand AnswerCommand { get; } 
        DictionaryEntry CurrentEntry { get; }
        bool IsInAnswerMode { get; }
        bool LastAnswerWasCorrect { get; }
        string Answer { get; }
        void Initialize(List<DictionaryEntry> dictionaryEntries);
    }
}
