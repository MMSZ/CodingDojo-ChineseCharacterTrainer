using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Library;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class MainWindowVM : ViewModel, IMainWindowVM
    {
        private readonly IMenuVM _menuVM;
        private IViewModel _content;
        private readonly IQuestionVM _questionVM;
        private readonly ISummaryVM _summaryVM;

        public MainWindowVM(IMenuVM menuVM, IQuestionVM questionVM, ISummaryVM summaryVM)
        {
            _questionVM = questionVM;
            _summaryVM = summaryVM;
            _menuVM = menuVM;

            _questionVM.QuestionsFinished += QuestionVMQuestionsFinished;
            _menuVM.OpenDictionaryRequested += MenuVMOpenDictionaryRequested;
            _summaryVM.UploadFinished += SummaryVMUploadFinished;

            Content = _menuVM;
        }

        public void Initialize()
        {
            _menuVM.Initialize();
        }

        private void SummaryVMUploadFinished(Highscore obj)
        {
            Content = _menuVM;
        }

        private void MenuVMOpenDictionaryRequested(Dictionary dictionary)
        {
            _questionVM.Initialize(dictionary.Entries);
            Content = _questionVM;
        }

        private void QuestionVMQuestionsFinished(QuestionResult questionResult)
        {
            _summaryVM.Initialize(_menuVM.SelectedDictionary, questionResult);
            Content = _summaryVM;
        }

        public IViewModel Content
        {
            get { return _content; }
            set { _content = value; RaisePropertyChanged(() => Content); }
        }
    }
}
