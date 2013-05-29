using ChineseCharacterTrainer.Model;
using System.Threading.Tasks;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class DictionaryImporter : IDictionaryImporter
    {
        private readonly ITextFileReader _textFileReader;
        private readonly IWordlistParser _wordlistParser;
        private readonly IRepository _dictionaryRepository;

        public DictionaryImporter(
            ITextFileReader textFileReader,
            IWordlistParser wordlistParser,
            IRepository dictionaryRepository)
        {
            _textFileReader = textFileReader;
            _wordlistParser = wordlistParser;
            _dictionaryRepository = dictionaryRepository;
        }

        public async Task<Dictionary> ImportAsync(string name, string fileName)
        {
            var result = await Task.Run(() =>
                {
                    var lines = _textFileReader.Read(fileName);
                    var entries = _wordlistParser.Import(lines);
                    var dictionary = new Dictionary(name, entries);
                    _dictionaryRepository.AddDictionary(dictionary);
                    return dictionary;
                });

            return result;
        }
    }
}