using ChineseCharacterTrainer.Model;
using System.Data.Entity.ModelConfiguration;

namespace ChineseCharacterTrainer.ServiceApp.Persistence
{
    public class TranslationMapping : EntityTypeConfiguration<Translation>
    {
        public TranslationMapping()
        {
            HasKey(p => p.Id);
        }
    }
}