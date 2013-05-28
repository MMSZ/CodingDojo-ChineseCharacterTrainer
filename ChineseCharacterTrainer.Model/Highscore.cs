using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract]
    [KnownType(typeof(User))]
    public class Highscore : Entity
    {
        [DataMember]
        public virtual User User { get; set; }

        [DataMember]
        public int Score { get; set; }
    }
}
