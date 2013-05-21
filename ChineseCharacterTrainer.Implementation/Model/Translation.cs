namespace ChineseCharacterTrainer.Implementation.Model
{
    public class Translation : Entity
    {
        public Translation(string value)
        {
            Value = value;
        }

        protected Translation() { }

        public string Value { get; private set; }
    }
}
