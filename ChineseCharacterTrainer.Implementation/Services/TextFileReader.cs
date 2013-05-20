using System.Collections.Generic;
using System.IO;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class TextFileReader : ITextFileReader
    {
        public List<string> Read(string fileName)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}