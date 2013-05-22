using System;
using System.Collections.Generic;
using System.Linq;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class PinyinBeautifier : IPinyinBeautifier
    {
        #region Fields

        private static readonly Dictionary<Tuple<char, char>, string> UnicodeLookup = 
            new Dictionary<Tuple<char, char>, string>
                {
                    {new Tuple<char, char>('a', '1'), "\u0101"},
                    {new Tuple<char, char>('e', '1'), "\u0113"},
                    {new Tuple<char, char>('i', '1'), "\u012B"},
                    {new Tuple<char, char>('o', '1'), "\u014D"},
                    {new Tuple<char, char>('u', '1'), "\u016B"},
                    {new Tuple<char, char>('ü', '1'), "\u01D6"},
                    {new Tuple<char, char>('a', '2'), "\u00E1"},
                    {new Tuple<char, char>('e', '2'), "\u00E9"},
                    {new Tuple<char, char>('i', '2'), "\u00ED"},
                    {new Tuple<char, char>('o', '2'), "\u00F3"},
                    {new Tuple<char, char>('u', '2'), "\u00FA"},
                    {new Tuple<char, char>('ü', '2'), "\u01D8"},
                    {new Tuple<char, char>('a', '3'), "\u01CE"},
                    {new Tuple<char, char>('e', '3'), "\u011B"},
                    {new Tuple<char, char>('i', '3'), "\u01D0"},
                    {new Tuple<char, char>('o', '3'), "\u01D2"},
                    {new Tuple<char, char>('u', '3'), "\u01D4"},
                    {new Tuple<char, char>('ü', '3'), "\u01DA"},
                    {new Tuple<char, char>('a', '4'), "\u00E0"},
                    {new Tuple<char, char>('e', '4'), "\u00E8"},
                    {new Tuple<char, char>('i', '4'), "\u00EC"},
                    {new Tuple<char, char>('o', '4'), "\u00F2"},
                    {new Tuple<char, char>('u', '4'), "\u00F9"},
                    {new Tuple<char, char>('ü', '4'), "\u01DC"},
                };
        private static readonly List<char> Vowels = new List<char> { 'a', 'e', 'i', 'o', 'u', 'ü' };

        private static readonly char[] Tones = new[] { '1', '2', '3', '4'};

        #endregion Fields

        #region Public Methods

        public string Beautify(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            input = input.ToLower();

            var words = SplitIntoWords(input);
            var result = string.Empty;
            foreach (var word in words)
            {
                var convertedWord = ConvertSingleWord(word);
                result += convertedWord;
            }

            return result;
        }

        private static IEnumerable<string> SplitIntoWords(string input)
        {
            var result = new List<string>();
            var currentWord = string.Empty;
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                currentWord += c;
                if (Tones.Contains(c) || i == input.Length - 1 || c == ' ')
                {
                    result.Add(currentWord);
                    currentWord = string.Empty;
                }
            }

            return result;
        }

        private static string ConvertSingleWord(string input)
        {
            var toneNumber = GetToneNumber(input);
            if (!Char.IsDigit(toneNumber))
            {
                return input;
            }

            var vowelToBeReplaced = GetVowelThatShouldBeReplaced(input);
            if (vowelToBeReplaced == ' ')
            {
                return input;
            }

            var vowelToReplaceWith = GetReplacementCharacter(vowelToBeReplaced, toneNumber);
            var result = RemoveTrailingNumber(input);
            result = ReplaceVowel(result, vowelToBeReplaced, vowelToReplaceWith);
            return result;
        }

        #endregion Public Methods

        #region Private Static Methods

        private static char FindFirstVowel(string input)
        {
            return FindSpecificVowelOccurence(input, 1);
        }

        private static char FindSecondVowel(string input)
        {
            return FindSpecificVowelOccurence(input, 2);
        }

        private static char FindSpecificVowelOccurence(string input, int occurence)
        {
            var vowelsFound = 0;
            foreach (var c in input)
            {
                if (Vowels.Contains(c)) vowelsFound++;
                if (vowelsFound == occurence) return c;
            }

            return ' ';
        }

        private static string GetReplacementCharacter(char vowel, char tone)
        {
            return UnicodeLookup[new Tuple<char, char>(vowel, tone)];
        }

        private static char GetToneNumber(string input)
        {
            return input[input.Length - 1];
        }

        private static char GetVowelThatShouldBeReplaced(string input)
        {
            var firstVowel = FindFirstVowel(input);
            if (NumberOfVowels(input) >= 2)
            {
                if (firstVowel == 'a' || firstVowel == 'e' || firstVowel == 'o')
                {
                    return firstVowel;
                }

                var secondVowel = FindSecondVowel(input);
                return secondVowel;
            }

            return firstVowel;
        }

        private static bool IsVowel(char character)
        {
            return Vowels.Contains(character);
        }

        private static int NumberOfVowels(string input)
        {
            return input.Count(IsVowel);
        }

        private static string ReplaceVowel(string input, char oldVowel, string newVowel)
        {
            var indexOfFirstOccurrence = input.IndexOf(oldVowel);
            var inputWithOldVowelRemoved = input.Remove(indexOfFirstOccurrence, 1);
            var inputWithNewVowelAdded = inputWithOldVowelRemoved.Insert(indexOfFirstOccurrence, newVowel);
            return inputWithNewVowelAdded;
        }

        private static string RemoveTrailingNumber(string input)
        {
            return input.Substring(0, input.Length - 1);
        }

        #endregion Private Static Methods
    }
}