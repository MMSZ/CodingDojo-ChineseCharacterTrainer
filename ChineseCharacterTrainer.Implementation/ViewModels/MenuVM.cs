using System;
using System.Collections.ObjectModel;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Library;
using System.Windows.Input;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class MenuVM : ViewModel, IMenuVM
    {
        private readonly IOpenFileDialog _openFileDialog;
        private readonly IDictionaryImporter _dictionaryImporter;
        private readonly IDictionaryRepository _dictionaryRepository;
        private IAsyncCommand _importCommand;
        private Dictionary _selectedDictionary;
        private ICommand _openCommand;
        private string _name;
        private string _fileName;
        private ICommand _browseCommand;

        public MenuVM(
            IOpenFileDialog openFileDialog,
            IDictionaryImporter dictionaryImporter,
            IDictionaryRepository dictionaryRepository)
        {
            _openFileDialog = openFileDialog;
            _dictionaryImporter = dictionaryImporter;
            _dictionaryRepository = dictionaryRepository;
            _openFileDialog.Filter = "Comma separated files (*.csv)|*.csv|All files (*.*)|*.*";

            AvailableDictionaries = new ObservableCollection<Dictionary>(_dictionaryRepository.GetAll());
        }

        public Dictionary SelectedDictionary
        {
            get { return _selectedDictionary; }
            set { _selectedDictionary = value; RaisePropertyChanged(() => SelectedDictionary); }
        }

        public ObservableCollection<Dictionary> AvailableDictionaries { get; private set; }

        public IAsyncCommand ImportCommand
        {
            get
            {
                return _importCommand ??
                       (_importCommand =
                        new RelayCommand(
                            async p =>
                                {
                                    var dictionary = await _dictionaryImporter.ImportAsync(Name, FileName);
                                    AvailableDictionaries.Add(dictionary);
                                },
                            p => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(FileName)));
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                return _openCommand ??
                       (_openCommand =
                        new RelayCommand(p => RaiseOpenDictionaryRequested(SelectedDictionary),
                                         p => SelectedDictionary != null));
            }
        }

        public ICommand BrowseCommand
        {
            get
            {
                return _browseCommand ??
                       (_browseCommand =
                        new RelayCommand(p =>
                            {
                                var result = _openFileDialog.ShowDialog();
                                if (result == true)
                                {
                                    FileName = _openFileDialog.FileName;
                                }
                            }));
            }
        }

        public event Action<Dictionary> OpenDictionaryRequested;

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; RaisePropertyChanged(() => FileName); }
        }

        protected virtual void RaiseOpenDictionaryRequested(Dictionary dictionary)
        {
            var handler = OpenDictionaryRequested;
            if (handler != null) handler(dictionary);
        }
    }
}
