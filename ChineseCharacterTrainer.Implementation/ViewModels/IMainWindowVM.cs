using ChineseCharacterTrainer.Library;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public interface IMainWindowVM : IViewModel
    {
        IViewModel Content { get; set; }
    }
}