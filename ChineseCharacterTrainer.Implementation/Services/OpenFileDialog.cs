using System.Diagnostics.CodeAnalysis;

namespace ChineseCharacterTrainer.Implementation.Services
{
    [ExcludeFromCodeCoverage]
    public class OpenFileDialog : IOpenFileDialog
    {
        private readonly Microsoft.Win32.OpenFileDialog _openFileDialog = new Microsoft.Win32.OpenFileDialog();

        public string FileName
        {
            get { return _openFileDialog.FileName; }
        }

        public string Filter
        {
            get { return _openFileDialog.Filter; }
            set { _openFileDialog.Filter = value; }
        }

        public bool? ShowDialog()
        {
            return _openFileDialog.ShowDialog();
        }
    }
}
