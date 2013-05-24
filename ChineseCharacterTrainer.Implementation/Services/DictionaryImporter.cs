using System.Threading.Tasks;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class DictionaryImporter : IDictionaryImporter
    {
        private readonly ITextFileReader _textFileReader;
        private readonly IWordlistParser _wordlistParser;
        private readonly IDictionaryRepository _dictionaryRepository;

        public DictionaryImporter(
            ITextFileReader textFileReader,
            IWordlistParser wordlistParser,
            IDictionaryRepository dictionaryRepository)
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
                    var dictionary = new Dictionary(name);//, entries);
                    _dictionaryRepository.Add(dictionary);
                    return dictionary;
                });

            return result;
        }
    }
}