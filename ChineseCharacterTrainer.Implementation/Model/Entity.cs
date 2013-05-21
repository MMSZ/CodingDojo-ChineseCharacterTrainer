using System;

namespace ChineseCharacterTrainer.Implementation.Model
{
    public abstract class Entity
    {
        public Guid Id { get;  protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
