using System;
using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract(IsReference = true)]
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
