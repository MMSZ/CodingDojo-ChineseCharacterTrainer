using ChineseCharacterTrainer.Model;
using System.Data.Entity.ModelConfiguration;

namespace ChineseCharacterTrainer.Implementation.Persistence
{
    public class DictionaryMapping : EntityTypeConfiguration<Dictionary>
    {
        public DictionaryMapping()
        {
            HasKey(p => p.Id);
        }
    }
}