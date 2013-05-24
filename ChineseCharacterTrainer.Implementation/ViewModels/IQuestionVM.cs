using System;
using System.Collections.Generic;
using System.Windows.Input;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Library;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public interface IQuestionVM : IViewModel
    {
        ICommand AnswerCommand { get; } 
        DictionaryEntry CurrentEntry { get; }
        bool IsInAnswerMode { get; }
        bool LastAnswerWasCorrect { get; }
        string Answer { get; }
        int NumberOfCorrectAnswers { get; }
        int NumberOfIncorrectAnswers { get; }
        void Initialize(List<DictionaryEntry> dictionaryEntries);
        event Action<QuestionResult> QuestionsFinished;
    }
}
