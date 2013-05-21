using System.Data.Entity;
using ChineseCharacterTrainer.Implementation.Persistence;

namespace ChineseCharacterTrainer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ChineseTrainerContext>());
        }
    }
}
