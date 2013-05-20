using System;
using System.Windows.Input;
using ChineseCharacterTrainer.Library;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public interface IMenuVM : IViewModel
    {
        ICommand BrowseCommand { get; }
        event EventHandler<FileImportRequestedEventArgs> FileImportRequested;
    }
}