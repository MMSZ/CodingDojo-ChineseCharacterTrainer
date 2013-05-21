using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.Services;
using Moq;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.Services
{
    public class DictionaryImporterTest
    {
        private IDictionaryImporter _objectUnderTest;
        private Mock<ITextFileReader> _textFileReaderMock;
        private Mock<IWordlistParser> _wordlistParserMock;
        private Mock<IDictionaryRepository> _dictionaryRepositoryMock;

        [SetUp]
        public void Initialize()
        {
            _textFileReaderMock = new Mock<ITextFileReader>();
            _wordlistParserMock = new Mock<IWordlistParser>();
            _dictionaryRepositoryMock = new Mock<IDictionaryRepository>();

            _objectUnderTest = new DictionaryImporter(
                _textFileReaderMock.Object, _wordlistParserMock.Object, _dictionaryRepositoryMock.Object);
        }


        [Test]
        public void ShouldReadFileWhenImportingFile()
        {
            ImportDictionary();

            _textFileReaderMock.Verify(p => p.Read("somefile.csv"), Times.Once());
        }

        [Test]
        public void ShouldParseLinesWhenImportingFile()
        {
            var lines = new List<string>();
            _textFileReaderMock.Setup(p => p.Read("somefile.csv")).Returns(lines);

            ImportDictionary();

            _wordlistParserMock.Verify(p => p.Import(lines), Times.Once());
        }

        [Test]
        public void ShouldAddDictionaryToRepositoryWhenImportingFile()
        {
            ImportDictionary();

            _dictionaryRepositoryMock.Verify(p => p.Add(It.IsAny<Dictionary>()));
        }

        private void ImportDictionary()
        {
            _objectUnderTest.Import("somename", "somefile.csv");
        }
    }
}
