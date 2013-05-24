using ChineseCharacterTrainer.Model;
using System.Threading.Tasks;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IDictionaryImporter
    {
        Task<Dictionary> ImportAsync(string name, string fileName);
    }
}