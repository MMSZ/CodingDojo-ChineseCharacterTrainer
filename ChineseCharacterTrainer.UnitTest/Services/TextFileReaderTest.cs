using System;
using System.IO;
using ChineseCharacterTrainer.Implementation.Services;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.Services
{
    public class TextFileReaderTest
    {
        private ITextFileReader _objectUnderTest;

        [SetUp]
        public void Initialize()
        {
            _objectUnderTest = new TextFileReader();    
        }

        [Test]
        public void ShouldReadAllLinesFromFile()
        {
            var lines = _objectUnderTest.Read(@"..\..\Testdata\testwordlist");

            Assert.AreEqual(2, lines.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _objectUnderTest.Read(null));
        }

        [Test]
        public void ShouldThrowExceptionWhenFileDoesNotExist()
        {
            Assert.Throws<FileNotFoundException>(() => _objectUnderTest.Read("invalidfile"));
        }
    }
}
