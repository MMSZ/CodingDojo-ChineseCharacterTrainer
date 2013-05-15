using System;

namespace ChineseCharacterTrainer.Implementation.Utilities
{
    public interface IServiceLocator
    {
        T Get<T>();
        object Get(Type service);
    }
}