using ChineseCharacterTrainer.Implementation.ViewModels;

namespace ChineseCharacterTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IMainWindowVM _mainWindowVM;

        public MainWindow()
        {
            InitializeComponent();

            _mainWindowVM = ServiceLocatorSingleton.Instance.Get<IMainWindowVM>();
            DataContext = _mainWindowVM;
        }

        protected override void OnActivated(System.EventArgs e)
        {
            _mainWindowVM.Initialize();

            base.OnActivated(e);
        }
    }
}
