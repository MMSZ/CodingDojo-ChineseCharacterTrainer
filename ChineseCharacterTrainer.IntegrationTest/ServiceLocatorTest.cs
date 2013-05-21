using System;
using ChineseCharacterTrainer.Implementation.Persistence;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Implementation.Utilities;
using ChineseCharacterTrainer.Implementation.ViewModels;
using NUnit.Framework;

namespace ChineseCharacterTrainer.IntegrationTest
{
    public class ServiceLocatorTest
    {
        [TestCase(typeof(IMainWindowVM), typeof(MainWindowVM))]
        [TestCase(typeof(IQuestionVM), typeof(QuestionVM))]
        [TestCase(typeof(ISummaryVM), typeof(SummaryVM))]
        [TestCase(typeof(IMenuVM), typeof(MenuVM))]
        [TestCase(typeof(IDateTime), typeof(DateTimeWrapper))]
        [TestCase(typeof(IServiceLocator), typeof(ServiceLocator))]
        [TestCase(typeof(IWordlistParser), typeof(WordlistParser))]
        [TestCase(typeof(ITextFileReader), typeof(TextFileReader))]
        [TestCase(typeof(IOpenFileDialog), typeof(OpenFileDialog))]
        [TestCase(typeof(IPinyinBeautifier), typeof(PinyinBeautifier))]
        [TestCase(typeof(IChineseTrainerContext), typeof(ChineseTrainerContext))]
        [TestCase(typeof(IDictionaryRepository), typeof(DictionaryRepository))]
        [TestCase(typeof(IDictionaryImporter), typeof(DictionaryImporter))]
        public void ShouldResolveDependencies(Type service, Type implementation)
        {
            var objectUnderTest = new ServiceLocator();

            var instance = objectUnderTest.Get(service);

            Assert.AreEqual(implementation, instance.GetType());
        }
    }
}
