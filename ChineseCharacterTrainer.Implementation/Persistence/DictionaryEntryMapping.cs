using System.Data.Entity.ModelConfiguration;
using ChineseCharacterTrainer.Implementation.Model;

namespace ChineseCharacterTrainer.Implementation.Persistence
{
    public class DictionaryEntryMapping : EntityTypeConfiguration<DictionaryEntry>
    {
        public DictionaryEntryMapping()
        {
            HasKey(p => p.Id);
        }
    }
}