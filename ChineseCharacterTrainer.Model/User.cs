using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract]
    [KnownType(typeof(Highscore))]
    public class User : Entity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public virtual List<Highscore> Highscores { get; set; } 
    }
}
