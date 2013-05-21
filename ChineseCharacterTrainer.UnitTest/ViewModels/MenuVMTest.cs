using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Implementation.ViewModels;
using Moq;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.ViewModels
{
    public class MenuVMTest
    {
        private IMenuVM _objectUnderTest;
        private Mock<IOpenFileDialog> _openFileDialogMock;
        private Mock<IDictionaryImporter> _dictionaryImporterMock;
        private Mock<IDictionaryRepository> _dictionaryRepositoryMock;

        [SetUp]
        public void Initialize()
        {
            _openFileDialogMock = new Mock<IOpenFileDialog>();
            _openFileDialogMock.SetupAllProperties();

            _dictionaryImporterMock = new Mock<IDictionaryImporter>();
            _dictionaryRepositoryMock = new Mock<IDictionaryRepository>();
            _dictionaryRepositoryMock.Setup(p => p.GetAll()).Returns(new List<Dictionary>());

            _objectUnderTest = CreateObjectUnderTest();
        }

        private IMenuVM CreateObjectUnderTest()
        {
            return new MenuVM(
                _openFileDialogMock.Object, _dictionaryImporterMock.Object, _dictionaryRepositoryMock.Object);
        }

        [Test]
        public void ShouldSetFilterToCsv()
        {
            Assert.AreEqual("Comma separated files (*.csv)|*.csv|All files (*.*)|*.*", _openFileDialogMock.Object.Filter);
        }

        [Test]
        public void ShouldImportFileWhenUserAcceptsFile()
        {
            _objectUnderTest.Name = "MyDict";
            _objectUnderTest.FileName = "somefile.csv";

            _objectUnderTest.ImportCommand.Execute(null);

            _dictionaryImporterMock.Verify(p => p.Import(_objectUnderTest.Name, _objectUnderTest.FileName));
        }

        [Test]
        public void ShouldBeAbleToImportWhenNameAndFileNameAreNotNull()
        {
            _objectUnderTest.Name = "MyDict";
            _objectUnderTest.FileName = "somefile.csv";

            var canExecute = _objectUnderTest.ImportCommand.CanExecute(null);

            Assert.IsTrue(canExecute);
        }

        [Test]
        public void ShouldNotBeAbleToImportWhenNameIsNull()
        {
            _objectUnderTest.FileName = "somefile.csv";

            var canExecute = _objectUnderTest.ImportCommand.CanExecute(null);

            Assert.IsFalse(canExecute);
        }

        [Test]
        public void ShouldNotBeAbleToImportWhenFileNameIsNull()
        {
            _objectUnderTest.Name = "MyDict";

            var canExecute = _objectUnderTest.ImportCommand.CanExecute(null);

            Assert.IsFalse(canExecute);
        }

        [Test]
        public void ShouldAddDictionaryToAvailableDictionariesWhenImportingFile()
        {
            _objectUnderTest.Name = "MyDict";
            _objectUnderTest.FileName = "somefile.csv";
            _openFileDialogMock.Setup(p => p.ShowDialog()).Returns(true);

            _objectUnderTest.ImportCommand.Execute(null);

            Assert.AreEqual(1, _objectUnderTest.AvailableDictionaries.Count);
        }

        [Test]
        public void ShouldInitializeAvailableDictionariesFromRepository()
        {
            var dictionaries = new List<Dictionary>
                {
                    new Dictionary("1", null),
                    new Dictionary("2", null)
                };
            _dictionaryRepositoryMock.Setup(p => p.GetAll()).Returns(dictionaries);

            _objectUnderTest = CreateObjectUnderTest();

            Assert.AreEqual(2, _objectUnderTest.AvailableDictionaries.Count);
        }

        [Test]
        public void ShouldBeAbleToOpenWhenDictionaryIsSelected()
        {
            _objectUnderTest.SelectedDictionary = new Dictionary("1", null);

            var canOpen = _objectUnderTest.OpenCommand.CanExecute(null);

            Assert.IsTrue(canOpen);
        }

        [Test]
        public void ShouldNotBeAbleToOpenWhenNoDictionaryIsSelected()
        {
            _objectUnderTest.SelectedDictionary = null;

            var canOpen = _objectUnderTest.OpenCommand.CanExecute(null);

            Assert.IsFalse(canOpen);
        }

        [Test]
        public void ShouldRaiseEventWhenDictionaryShouldBeOpened()
        {
            _objectUnderTest.SelectedDictionary = new Dictionary("1", null);
            Dictionary dictionaryToOpen = null;
            _objectUnderTest.OpenDictionaryRequested += dictionary => dictionaryToOpen = dictionary;

            _objectUnderTest.OpenCommand.Execute(null);

            Assert.AreEqual(_objectUnderTest.SelectedDictionary, dictionaryToOpen);
        }

        [Test]
        public void ShouldOpenFileDialogWhenBrowseCommandExecutes()
        {
            _objectUnderTest.BrowseCommand.Execute(null);

            _openFileDialogMock.Verify(p => p.ShowDialog());
        }

        [Test]
        public void ShouldSetFileNameAfterUserAcceptsFile()
        {
            _openFileDialogMock.Setup(p => p.ShowDialog()).Returns(true);
            _openFileDialogMock.Setup(p => p.FileName).Returns("somefile.csv");

            _objectUnderTest.BrowseCommand.Execute(null);

            Assert.AreEqual("somefile.csv", _objectUnderTest.FileName);
        }

        [Test]
        public void ShouldNotSetFileNameAfterUserCancelsFileDialog()
        {
            _openFileDialogMock.Setup(p => p.ShowDialog()).Returns(false);

            _objectUnderTest.BrowseCommand.Execute(null);

            Assert.AreEqual(null, _objectUnderTest.FileName);
        }
    }
}
