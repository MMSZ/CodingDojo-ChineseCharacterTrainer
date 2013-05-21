using ChineseCharacterTrainer.Implementation.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.UnitTest.Services
{
    public class WordlistParserTest
    {
        private static readonly List<string> ValidLines = new List<string> {"走,zou3,go;run", "你,ni3,you"};
        private static readonly List<string> ValidLineWithWhitespaces = new List<string> { " 走 , zou3 , go ; run " };

        [Test]
        public void ShouldReadFileIntoListOfWords()
        {
            var wordlistImporter = new WordlistParser();

            var wordList = wordlistImporter.Import(ValidLines);

            Assert.AreEqual(2, wordList.Count);
        }

        [Test]
        public void ShouldReadChineseCharacters()
        {
            var wordlistImporter = new WordlistParser();

            var wordList = wordlistImporter.Import(ValidLines);

            Assert.AreEqual("走", wordList[0].ChineseCharacters);
            Assert.AreEqual("你", wordList[1].ChineseCharacters);
        }

        [Test]
        public void ShouldReadPinyin()
        {
            var wordlistImporter = new WordlistParser();

            var wordList = wordlistImporter.Import(ValidLines);

            Assert.AreEqual("zou3", wordList[0].Pinyin);
            Assert.AreEqual("ni3", wordList[1].Pinyin);
        }

        [Test]
        public void ShouldReadTranslations()
        {
            var wordlistImporter = new WordlistParser();

            var wordList = wordlistImporter.Import(ValidLines);

            Assert.AreEqual(2, wordList[0].Translations.Count);
            Assert.AreEqual("go", wordList[0].Translations[0].Value);
            Assert.AreEqual("run", wordList[0].Translations[1].Value);
            Assert.AreEqual(1, wordList[1].Translations.Count);
            Assert.AreEqual("you", wordList[1].Translations[0].Value);
        }

        [Test]
        public void ShouldTrimPinyin()
        {
            var wordlistImporter = new WordlistParser();

            var wordList = wordlistImporter.Import(ValidLineWithWhitespaces);

            Assert.AreEqual("zou3", wordList[0].Pinyin);
        }

        [Test]
        public void ShouldTrimChineseCharacters()
        {
            var wordlistImporter = new WordlistParser();

            var wordList = wordlistImporter.Import(ValidLineWithWhitespaces);

            Assert.AreEqual("走", wordList[0].ChineseCharacters);
        }

        [Test]
        public void ShouldTrimTranslations()
        {
            var wordlistImporter = new WordlistParser();

            var wordList = wordlistImporter.Import(ValidLineWithWhitespaces);

            Assert.AreEqual("go", wordList[0].Translations[0].Value);
            Assert.AreEqual("run", wordList[0].Translations[1].Value);
        }

        [Test]
        public void ShouldReturnEmptyListForNullImport()
        {
            var wordlistImporter = new WordlistParser();

            var wordList = wordlistImporter.Import(null);

            Assert.AreEqual(0, wordList.Count);
        }

        [TestCase("SingleWord")]
        [TestCase("Two,Words")]
        [TestCase(",,")]
        [TestCase(" , , ")]
        [TestCase("ok,ok,;")]
        [TestCase("ok,ok,")]
        [TestCase("ok,,ok")]
        [TestCase(",ok,ok")]
        [TestCase(null)]
        public void ShouldRemoveInvalidLines(string line)
        {
            var wordlistImporter = new WordlistParser();

            var wordList = wordlistImporter.Import(new List<string> {line});

            Assert.AreEqual(0, wordList.Count);
        }
    }
}
