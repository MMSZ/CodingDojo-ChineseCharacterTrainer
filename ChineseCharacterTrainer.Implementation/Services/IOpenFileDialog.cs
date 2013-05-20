namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IOpenFileDialog
    {
        string FileName { get; }
        string Filter { get; set; }
        bool? ShowDialog();
    }
}