using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Library;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class MainWindowVM : ViewModel
    {
        private readonly IQuestionVM _questionVM;
        private IViewModel _content;

        public MainWindowVM(IQuestionVM questionVM)
        {
            _questionVM = questionVM;

            var dictionaryEntries = new List<DictionaryEntry>
                {
                    new DictionaryEntry("你", "ni3"),
                    new DictionaryEntry("不", "bu4"),
                    new DictionaryEntry("车", "che1")
                };

            _questionVM.Initialize(dictionaryEntries);

            Content = _questionVM;
        }

        public IViewModel Content
        {
            get { return _content; }
            set { _content = value; RaisePropertyChanged(() => Content); }
        }
    }
}
