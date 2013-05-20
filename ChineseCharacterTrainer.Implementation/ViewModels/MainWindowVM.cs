using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Library;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class MainWindowVM : ViewModel, IMainWindowVM
    {
        private readonly IMenuVM _menuVM;
        private IViewModel _content;
        private readonly IQuestionVM _questionVM;
        private readonly ISummaryVM _summaryVM;
        private readonly ITextFileReader _textFileReader;
        private readonly IWordlistParser _wordlistParser;

        public MainWindowVM(
            IMenuVM menuVM,
            IQuestionVM questionVM,
            ISummaryVM summaryVM,
            ITextFileReader textFileReader,
            IWordlistParser wordlistParser)
        {
            _questionVM = questionVM;
            _summaryVM = summaryVM;
            _textFileReader = textFileReader;
            _wordlistParser = wordlistParser;
            _menuVM = menuVM;

            _menuVM.FileImportRequested += MenuVMFileImportRequested;
            _questionVM.QuestionsFinished += QuestionVMQuestionsFinished;

            Content = _menuVM;
        }

        private void MenuVMFileImportRequested(object sender, FileImportRequestedEventArgs e)
        {
            var lines = _textFileReader.Read(e.FileName);
            var entries = _wordlistParser.Import(lines);
            _questionVM.Initialize(entries);
            Content = _questionVM;
        }

        private void QuestionVMQuestionsFinished(object sender, QuestionsFinishedEventArgs e)
        {
            _summaryVM.Initialize(e.QuestionResult);
            Content = _summaryVM;
        }

        public IViewModel Content
        {
            get { return _content; }
            set { _content = value; RaisePropertyChanged(() => Content); }
        }
    }
}
