using System;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Library;
using System.Windows.Input;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class MenuVM :  ViewModel,IMenuVM
    {
        private readonly IOpenFileDialog _openFileDialog;
        private ICommand _browseCommand;

        public MenuVM(IOpenFileDialog openFileDialog)
        {
            _openFileDialog = openFileDialog;
            _openFileDialog.Filter = "Comma separated files (*.csv)|*.csv|All files (*.*)|*.*";
        }

        public ICommand BrowseCommand { get
        {
            return _browseCommand ??
                   (_browseCommand =
                    new RelayCommand(
                        p =>
                            {
                                var result = _openFileDialog.ShowDialog();
                                if (result == true)
                                {
                                    RaiseFileImportRequested(new FileImportRequestedEventArgs(_openFileDialog.FileName));
                                }
                            }));
        }}

        public event EventHandler<FileImportRequestedEventArgs> FileImportRequested;

        public void RaiseFileImportRequested(FileImportRequestedEventArgs e)
        {
            var handler = FileImportRequested;
            if (handler != null) handler(this, e);
        }
    }
}
