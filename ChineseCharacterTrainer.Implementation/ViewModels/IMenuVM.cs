using System;
using System.Collections.ObjectModel;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Library;
using System.Windows.Input;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public interface IMenuVM : IViewModel
    {
        Dictionary SelectedDictionary { get; set; }
        ObservableCollection<Dictionary> AvailableDictionaries { get; } 
        ICommand ImportCommand { get; }
        ICommand OpenCommand { get; }
        ICommand BrowseCommand { get; }
        event Action<Dictionary> OpenDictionaryRequested;
        string Name { get; set; }
        string FileName { get; set; }
    }
}