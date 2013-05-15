using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.Utilities;
using ChineseCharacterTrainer.Library;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class MainWindowVM : ViewModel, IMainWindowVM
    {
        private readonly IServiceLocator _serviceLocator;
        private IViewModel _content;

        public MainWindowVM(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;

            var dictionaryEntries = new List<DictionaryEntry>
                {
                    new DictionaryEntry("你", "ni3"),
                    new DictionaryEntry("不", "bu4"),
                    new DictionaryEntry("车", "che1")
                };

            var questionVM = _serviceLocator.Get<IQuestionVM>();
            questionVM.Initialize(dictionaryEntries);
            questionVM.QuestionsFinished += QuestionVMQuestionsFinished;

            Content = questionVM;
        }

        private void QuestionVMQuestionsFinished(object sender, QuestionsFinishedEventArgs e)
        {
            (sender as IQuestionVM).QuestionsFinished -= QuestionVMQuestionsFinished;
            var summaryVM = _serviceLocator.Get<ISummaryVM>();
            summaryVM.Initialize(e.QuestionResult);
            Content = summaryVM;
        }

        public IViewModel Content
        {
            get { return _content; }
            set { _content = value; RaisePropertyChanged(() => Content); }
        }
    }
}
