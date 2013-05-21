using ChineseCharacterTrainer.Implementation.Model;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IDictionaryImporter
    {
        Dictionary Import(string name, string fileName);
    }
}