using System.Data.Entity.ModelConfiguration;
using ChineseCharacterTrainer.Implementation.Model;

namespace ChineseCharacterTrainer.Implementation.Persistence
{
    public class TranslationMapping : EntityTypeConfiguration<Translation>
    {
        public TranslationMapping()
        {
            HasKey(p => p.Id);
        }
    }
}