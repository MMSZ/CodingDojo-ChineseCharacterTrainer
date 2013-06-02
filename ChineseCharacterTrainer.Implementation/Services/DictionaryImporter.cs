using System.Linq;
using ChineseCharacterTrainer.Model;
using System.Threading.Tasks;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class DictionaryImporter : IDictionaryImporter
    {
        private readonly ITextFileReader _textFileReader;
        private readonly IWordlistParser _wordlistParser;
        private readonly IRepository _repository;

        public DictionaryImporter(
            ITextFileReader textFileReader,
            IWordlistParser wordlistParser,
            IRepository repository)
        {
            _textFileReader = textFileReader;
            _wordlistParser = wordlistParser;
            _repository = repository;
        }

        public async Task<Dictionary> ImportAsync(string name, string fileName)
        {
            var result = await Task.Run(() =>
                {
                    var lines = _textFileReader.Read(fileName);
                    var entries = _wordlistParser.Import(lines);
                    var dictionary = new Dictionary(name, entries);
                    AddDictionary(dictionary);
                    return dictionary;
                });

            return result;
        }

        private void AddDictionary(Dictionary dictionary)
        {
            //// Note: This is a workaround to avoid exceeding the maximum message size in WCF.
            var entries = dictionary.Entries;
            dictionary.Entries = null;

            _repository.Add(dictionary);

            foreach (var dictionaryEntry in entries)
            {
                _repository.Add(dictionaryEntry);
            }

            dictionary.Entries = entries;
        }
    }
}