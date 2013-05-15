using System;
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
        [TestCase(typeof(IDateTime), typeof(DateTimeWrapper))]
        [TestCase(typeof(IServiceLocator), typeof(ServiceLocator))]
        public void ShouldResolveDependencies(Type service, Type implementation)
        {
            var objectUnderTest = new ServiceLocator();

            var instance = objectUnderTest.Get(service);

            Assert.AreEqual(implementation, instance.GetType());
        }
    }
}
