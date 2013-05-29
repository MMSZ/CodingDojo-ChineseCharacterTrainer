using ChineseCharacterTrainer.Model;
using System.Data.Entity.ModelConfiguration;

namespace ChineseCharacterTrainer.ServiceApp.Persistence
{
    public class HighscoreMapping : EntityTypeConfiguration<Highscore>
    {
        public HighscoreMapping()
        {
            HasKey(p => p.Id);
            HasRequired(p => p.User).WithMany(p => p.Highscores).HasForeignKey(p => p.UserId);
            HasRequired(p => p.Dictionary).WithMany(p => p.Highscores).HasForeignKey(p => p.DictionaryId);
        }
    }
}