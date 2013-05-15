using System;
using ChineseCharacterTrainer.Implementation.Model;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class QuestionsFinishedEventArgs : EventArgs
    {
        public QuestionsFinishedEventArgs(QuestionResult questionResult)
        {
            QuestionResult = questionResult;
        }

        public QuestionResult QuestionResult { get; private set; }
    }
}