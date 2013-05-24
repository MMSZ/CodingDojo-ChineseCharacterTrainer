using ChineseCharacterTrainer.Model;
using System.Data.Entity.ModelConfiguration;

namespace ChineseCharacterTrainer.ServiceApp.Persistence
{
    public class DictionaryMapping : EntityTypeConfiguration<Dictionary>
    {
        public DictionaryMapping()
        {
            HasKey(p => p.Id);
        }
    }
}