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
        private readonly IHighscoreVM _highscoreVM;

        public MainWindowVM(IMenuVM menuVM, IQuestionVM questionVM, ISummaryVM summaryVM, IHighscoreVM highscoreVM)
        {
            _questionVM = questionVM;
            _summaryVM = summaryVM;
            _highscoreVM = highscoreVM;
            _menuVM = menuVM;

            _questionVM.QuestionsFinished += QuestionVMQuestionsFinished;
            _menuVM.OpenDictionaryRequested += MenuVMOpenDictionaryRequested;
            _summaryVM.UploadFinished += SummaryVMUploadFinished;
            _highscoreVM.ReturnToMenuRequested += HighscoreVMReturnToMenuRequested;

            Content = _menuVM;
        }

        public void Initialize()
        {
            _menuVM.Initialize();
        }

        private void HighscoreVMReturnToMenuRequested()
        {
            Content = _menuVM;
        }

        private void SummaryVMUploadFinished(Highscore highscore)
        {
            _highscoreVM.Initialize(highscore);
            Content = _highscoreVM;
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
