﻿using ChineseCharacterTrainer.Implementation.ServiceReference;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Implementation.Utilities;
using ChineseCharacterTrainer.Implementation.ViewModels;
using ChineseCharacterTrainer.ServiceApp.Persistence;
using Moq;
using NUnit.Framework;
using System;

namespace ChineseCharacterTrainer.IntegrationTest
{
    public class ServiceLocatorTest
    {
        [TestCase(typeof(IMainWindowVM), typeof(MainWindowVM))]
        [TestCase(typeof(IQuestionVM), typeof(QuestionVM))]
        [TestCase(typeof(ISummaryVM), typeof(SummaryVM))]
        [TestCase(typeof(IMenuVM), typeof(MenuVM))]
        [TestCase(typeof(IHighscoreVM), typeof(HighscoreVM))]
        [TestCase(typeof(IDateTime), typeof(DateTimeWrapper))]
        [TestCase(typeof(IServiceLocator), typeof(ServiceLocator))]
        [TestCase(typeof(IWordlistParser), typeof(WordlistParser))]
        [TestCase(typeof(ITextFileReader), typeof(TextFileReader))]
        [TestCase(typeof(IOpenFileDialog), typeof(OpenFileDialog))]
        [TestCase(typeof(IPinyinBeautifier), typeof(PinyinBeautifier))]
        [TestCase(typeof(IChineseTrainerContext), typeof(ChineseTrainerContext))]
        [TestCase(typeof(IRepository), typeof(Repository))]
        [TestCase(typeof(IDictionaryImporter), typeof(DictionaryImporter))]
        [TestCase(typeof(IDictionaryEntryPicker), typeof(RandomDictionaryEntryPicker))]
        [TestCase(typeof(IEnumerableShuffler), typeof(EnumerableShuffler))]
        [TestCase(typeof(IScoreCalculator), typeof(ScoreCalculator))]
        public void ShouldResolveDependencies(Type service, Type implementation)
        {
            var objectUnderTest = new ServiceLocator();

            objectUnderTest.ReplaceBind(typeof (IChineseCharacterTrainerService),
                                        new Mock<IChineseCharacterTrainerService>().Object);

            var instance = objectUnderTest.Get(service);

            Assert.AreEqual(implementation, instance.GetType());
        }
    }
}
