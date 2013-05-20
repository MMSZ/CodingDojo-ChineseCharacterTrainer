using System;
using System.Diagnostics.CodeAnalysis;

namespace ChineseCharacterTrainer.Implementation.Utilities
{
    [ExcludeFromCodeCoverage]
    public class DateTimeWrapper : IDateTime
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}
