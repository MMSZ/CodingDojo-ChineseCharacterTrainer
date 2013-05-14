using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.ViewModels;
using Moq;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.ViewModels
{
    public class MainWindowVMTest
    {
        [Test]
        public void ShouldSetContentToQuestionViewModelInConstructor()
        {
            var questionVMMock = new Mock<IQuestionVM>();

            var objectUnderTest = new MainWindowVM(questionVMMock.Object);

            Assert.AreEqual(questionVMMock.Object, objectUnderTest.Content);
        }

        [Test]
        public void ShouldInitializeQuestionViewModelInConstructor()
        {
            var questionVMMock = new Mock<IQuestionVM>();

            new MainWindowVM(questionVMMock.Object);

            questionVMMock.Verify(p=>p.Initialize(It.IsAny<List<DictionaryEntry>>()));
        }
    }
}
