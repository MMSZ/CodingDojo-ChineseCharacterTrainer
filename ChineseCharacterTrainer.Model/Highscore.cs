using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract(IsReference = true)]
    public class Highscore : Entity
    {
        public Highscore(User user, Dictionary dictionary, int score)
        {
            User = user;
            Dictionary = dictionary;
            Score = score;
        }

        protected Highscore() { }

        [DataMember]
        public virtual User User { get; private set; }

        [DataMember]
        public virtual Dictionary Dictionary { get; private set; }

        [DataMember]
        public int Score { get; private set; }
    }
}
