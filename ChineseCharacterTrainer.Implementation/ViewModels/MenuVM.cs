using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Library;
using ChineseCharacterTrainer.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class MenuVM : ViewModel, IMenuVM
    {
        private readonly IOpenFileDialog _openFileDialog;
        private readonly IDictionaryImporter _dictionaryImporter;
        private readonly IRepository _dictionaryRepository;
        private IAsyncCommand _importCommand;
        private Dictionary _selectedDictionary;
        private ICommand _openCommand;
        private string _name;
        private string _fileName;
        private ICommand _browseCommand;
        private ObservableCollection<Dictionary> _availableDictionaries;

        public MenuVM(
            IOpenFileDialog openFileDialog,
            IDictionaryImporter dictionaryImporter,
            IRepository dictionaryRepository)
        {
            _openFileDialog = openFileDialog;
            _dictionaryImporter = dictionaryImporter;
            _dictionaryRepository = dictionaryRepository;
            _openFileDialog.Filter = "Comma separated files (*.csv)|*.csv|All files (*.*)|*.*";

            AvailableDictionaries = new ObservableCollection<Dictionary>();
        }

        public Dictionary SelectedDictionary
        {
            get { return _selectedDictionary; }
            set { _selectedDictionary = value; RaisePropertyChanged(() => SelectedDictionary); }
        }

        public ObservableCollection<Dictionary> AvailableDictionaries
        {
            get { return _availableDictionaries; }
            private set { _availableDictionaries = value; RaisePropertyChanged(() => AvailableDictionaries); }
        }

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

        public async Task Initialize()
        {
            var dictionaries = await Task.Run(() =>_dictionaryRepository.GetAll<Dictionary>());
            AvailableDictionaries = new ObservableCollection<Dictionary>(dictionaries);
        }

        protected virtual void RaiseOpenDictionaryRequested(Dictionary dictionary)
        {
            var handler = OpenDictionaryRequested;
            if (handler != null) handler(dictionary);
        }
    }
}
