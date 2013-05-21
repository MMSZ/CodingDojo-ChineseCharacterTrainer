using System.Threading.Tasks;
using ChineseCharacterTrainer.Implementation.Model;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IDictionaryImporter
    {
        Task<Dictionary> ImportAsync(string name, string fileName);
    }
}