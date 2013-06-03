using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChineseCharacterTrainer.UnitTest.Services
{
    public class DictionaryImporterTest
    {
        private IDictionaryImporter _objectUnderTest;
        private Mock<ITextFileReader> _textFileReaderMock;
        private Mock<IWordlistParser> _wordlistParserMock;
        private Mock<IRepository> _dictionaryRepositoryMock;

        [SetUp]
        public void Initialize()
        {
            _textFileReaderMock = new Mock<ITextFileReader>();

            _wordlistParserMock = new Mock<IWordlistParser>();
            _wordlistParserMock.Setup(p => p.Import(It.IsAny<IEnumerable<string>>()))
                               .Returns(new List<DictionaryEntry>());

            _dictionaryRepositoryMock = new Mock<IRepository>();

            _objectUnderTest = new DictionaryImporter(
                _textFileReaderMock.Object, _wordlistParserMock.Object, _dictionaryRepositoryMock.Object);
        }


        [Test]
        public async void ShouldReadFileWhenImportingFile()
        {
            await ImportDictionary();

            _textFileReaderMock.Verify(p => p.Read("somefile.csv"), Times.Once());
        }

        [Test]
        public async void ShouldParseLinesWhenImportingFile()
        {
            var lines = new List<string>();
            _textFileReaderMock.Setup(p => p.Read("somefile.csv")).Returns(lines);

            await ImportDictionary();

            _wordlistParserMock.Verify(p => p.Import(lines), Times.Once());
        }

        [Test]
        public async void ShouldAddDictionaryToRepositoryWhenImportingFile()
        {
            await ImportDictionary();

            _dictionaryRepositoryMock.Verify(p => p.Add(It.IsAny<Dictionary>()));
        }

        private Task<Dictionary> ImportDictionary()
        {
            return _objectUnderTest.ImportAsync("somename", "somefile.csv");
        }
    }
}
