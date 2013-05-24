using ChineseCharacterTrainer.Model;
using System.Data.Entity.ModelConfiguration;

namespace ChineseCharacterTrainer.ServiceApp.Persistence
{
    public class DictionaryEntryMapping : EntityTypeConfiguration<DictionaryEntry>
    {
        public DictionaryEntryMapping()
        {
            HasKey(p => p.Id);
            HasMany(p => p.Translations).WithRequired(p => p.DictionaryEntry);
        }
    }
}