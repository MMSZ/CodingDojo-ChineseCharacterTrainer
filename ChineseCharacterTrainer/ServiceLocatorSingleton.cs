using ChineseCharacterTrainer.Implementation.Utilities;

namespace ChineseCharacterTrainer
{
    public static class ServiceLocatorSingleton
    {
        static ServiceLocatorSingleton()
        {
            Instance = new ServiceLocator();
        }

        public static IServiceLocator Instance { get; private set; }
    }
}
