using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Implementation.ViewModels;
using ChineseCharacterTrainer.ServiceApp.Persistence;
using Ninject;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ChineseCharacterTrainer.Implementation.Utilities
{
    [ExcludeFromCodeCoverage]
    public class ServiceLocator : IServiceLocator
    {
        private readonly StandardKernel _standardKernel;

        public ServiceLocator()
        {
            _standardKernel = new StandardKernel();

            _standardKernel.Bind<IMainWindowVM>().To<MainWindowVM>().InSingletonScope();
            _standardKernel.Bind<IMenuVM>().To<MenuVM>().InSingletonScope();
            _standardKernel.Bind<IQuestionVM>().To<QuestionVM>().InSingletonScope();
            _standardKernel.Bind<ISummaryVM>().To<SummaryVM>().InSingletonScope();

            _standardKernel.Bind<IWordlistParser>().To<WordlistParser>().InSingletonScope();
            _standardKernel.Bind<ITextFileReader>().To<TextFileReader>().InSingletonScope();
            _standardKernel.Bind<IPinyinBeautifier>().To<PinyinBeautifier>().InSingletonScope();
            _standardKernel.Bind<IDictionaryRepository>().To<DictionaryRepository>().InSingletonScope();
            _standardKernel.Bind<IDictionaryImporter>().To<DictionaryImporter>().InSingletonScope();
            
            _standardKernel.Bind<IDateTime>().To<DateTimeWrapper>();
            _standardKernel.Bind<IOpenFileDialog>().To<OpenFileDialog>();

            _standardKernel.Bind<IChineseTrainerContext>().To<ChineseTrainerContext>().InSingletonScope();

            _standardKernel.Bind<IServiceLocator>().ToConstant(this);
        }

        public T Get<T>()
        {
            return _standardKernel.Get<T>();
        }

        public object Get(Type service)
        {
            return _standardKernel.Get(service);
        }
    }
}