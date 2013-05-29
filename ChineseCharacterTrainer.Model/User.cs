using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract(IsReference = true)]
    public class User : Entity
    {
        public User(string name)
        {
            Name = name;
        }

        protected User() { }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public virtual List<Highscore> Highscores { get; private set; } 
    }
}
