using ChineseCharacterTrainer.Implementation.ViewModels;
using System.Windows.Input;

namespace ChineseCharacterTrainer
{
    public partial class QuestionView
    {
        public QuestionView()
        {
            InitializeComponent();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (DataContext as IQuestionVM).AnswerCommand.Execute(null);
            }
        }
    }
}
