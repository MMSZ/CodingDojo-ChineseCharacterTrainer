using System;

namespace ChineseCharacterTrainer.Implementation.Utilities
{
    public class DateTimeWrapper : IDateTime
    {
        public DateTime Now { get { return DateTime.Now; } }
    }

    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
