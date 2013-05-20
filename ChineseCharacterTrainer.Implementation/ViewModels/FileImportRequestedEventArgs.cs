using System;

namespace ChineseCharacterTrainer.Implementation.ViewModels
{
    public class FileImportRequestedEventArgs : EventArgs
    {
        public string FileName { get; private set; }

        public FileImportRequestedEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }
}