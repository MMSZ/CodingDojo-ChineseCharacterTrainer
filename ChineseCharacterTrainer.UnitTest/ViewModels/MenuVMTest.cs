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

        [SetUp]
        public void Initialize()
        {
            _openFileDialogMock = new Mock<IOpenFileDialog>();
            _openFileDialogMock.SetupAllProperties();

            _objectUnderTest = new MenuVM(_openFileDialogMock.Object);
        }

        [Test] 
        public void ShouldShowFileBrowseDialogWhenBrowseCommandIsExecuted()
        {
            _objectUnderTest.BrowseCommand.Execute(null);

            _openFileDialogMock.Verify(p => p.ShowDialog(), Times.Once());
        }

        [Test]
        public void ShouldRaiseEventWhenUserAcceptsAFile()
        {
            var fileName = string.Empty;
            _openFileDialogMock.Setup(p => p.FileName).Returns("somefile.csv");
            _openFileDialogMock.Setup(p => p.ShowDialog()).Returns(true);
            _objectUnderTest.FileImportRequested += (sender, args) =>fileName = args.FileName;

            _objectUnderTest.BrowseCommand.Execute(null);

            Assert.AreEqual("somefile.csv", fileName);
        }

        [Test]
        public void ShouldSetFilterToCsv()
        {
            Assert.AreEqual("Comma separated files (*.csv)|*.csv|All files (*.*)|*.*", _openFileDialogMock.Object.Filter);
        }
    }
}
