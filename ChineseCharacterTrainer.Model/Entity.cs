using System;
using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract]
    public abstract class Entity
    {
        [DataMember]
        public Guid Id { get;  protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
