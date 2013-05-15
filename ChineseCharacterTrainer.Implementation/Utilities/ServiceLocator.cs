using System;
using ChineseCharacterTrainer.Implementation.ViewModels;
using Ninject;

namespace ChineseCharacterTrainer.Implementation.Utilities
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly StandardKernel _standardKernel;

        public ServiceLocator()
        {
            _standardKernel = new StandardKernel();

            _standardKernel.Bind<IMainWindowVM>().To<MainWindowVM>().InSingletonScope();
            _standardKernel.Bind<IQuestionVM>().To<QuestionVM>().InSingletonScope();
            _standardKernel.Bind<ISummaryVM>().To<SummaryVM>().InSingletonScope();

            _standardKernel.Bind<IDateTime>().To<DateTimeWrapper>();

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