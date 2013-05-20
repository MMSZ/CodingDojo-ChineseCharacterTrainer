using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface ITextFileReader
    {
        List<string> Read(string fileName);
    }
}