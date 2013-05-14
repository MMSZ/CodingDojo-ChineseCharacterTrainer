using ChineseCharacterTrainer.Implementation.ViewModels;

namespace ChineseCharacterTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var questionVM = new QuestionVM();
            DataContext = new MainWindowVM(questionVM);
        }
    }
}
