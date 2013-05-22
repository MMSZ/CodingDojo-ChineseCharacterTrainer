using ChineseCharacterTrainer.Implementation.Services;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.Services
{
    public class PinyinBeautifierTest
    {
        #region Unicode characters
        ////a 	ā = U+0101 	á = U+00E1 	ǎ = U+01CE 	à = U+00E0
        ////e 	ē = U+0113 	é = U+00E9 	ě = U+011B 	è = U+00E8
        ////i 	ī = U+012B 	í = U+00ED 	ǐ = U+01D0 	ì = U+00EC
        ////o 	ō = U+014D 	ó = U+00F3 	ǒ = U+01D2 	ò = U+00F2
        ////u 	ū = U+016B 	ú = U+00FA 	ǔ = U+01D4 	ù = U+00F9
        ////ü 	ǖ = U+01D6 	ǘ = U+01D8 	ǚ = U+01DA 	ǜ = U+01DC
        #endregion

        #region Test Methods

        #region TestCases
        [TestCase("leng3", "lěng")]
        [TestCase("chan1", "chān")]
        [TestCase("ba", "ba")]
        [TestCase("brrr3", "brrr3")]
        [TestCase("", "")]
        [TestCase(null, null)]
        [TestCase(" ", " ")]
        [TestCase("bn1e", "bn1e")]
        [TestCase("kuai4", "kuài")]
        [TestCase("ben1e", "bēne")]
        [TestCase("ben1e1", "bēnē")]
        [TestCase("bene1", "bēne")]
        [TestCase("HUA1", "huā")]
        [TestCase("hua hua", "hua hua")]
        [TestCase("hua hua1", "hua huā")]
        [TestCase("na'er3", "nǎ'er")]
        [TestCase("na3'er", "nǎ'er")]
        #endregion
        public void ShouldConvertCornerCases(string input, string expected)
        {
            AssertConversion(input, expected);
        }

        #region TestCases
        [TestCase("ba4ba", "bàba")]
        [TestCase("xie4xie", "xièxie")]
        [TestCase("chu1zu1che1", "chūzūchē")]
        #endregion
        public void ShouldConvertMultipleWords(string input, string expected)
        {
            AssertConversion(input, expected);
        }

        #region Testcases
        [TestCase("mao1", "māo")]
        [TestCase("bei3", "běi")]
        [TestCase("chou1", "chōu")]
        [TestCase("huo2", "huó")]
        [TestCase("xie4", "xiè")] 
        #endregion
        public void ShouldConvertDoubleVowels(string input, string expected)
        {
            AssertConversion(input, expected);
        }

        #region Testcases
        [TestCase("ma1", "mā")]
        [TestCase("he1", "hē")]
        [TestCase("yi1", "yī")]
        [TestCase("ho1", "hō")]
        [TestCase("mu1", "mū")]
        [TestCase("nü1", "nǖ")]
        [TestCase("ma2", "má")]
        [TestCase("ne2", "né")]
        [TestCase("li2", "lí")]
        [TestCase("po2", "pó")]
        [TestCase("hu2", "hú")]
        [TestCase("nü2", "nǘ")]
        [TestCase("ma3", "mǎ")]
        [TestCase("ne3", "ně")]
        [TestCase("li3", "lǐ")]
        [TestCase("po3", "pǒ")]
        [TestCase("hu3", "hǔ")]
        [TestCase("nü3", "nǚ")]
        [TestCase("ma4", "mà")]
        [TestCase("ne4", "nè")]
        [TestCase("li4", "lì")]
        [TestCase("po4", "pò")]
        [TestCase("hu4", "hù")]
        [TestCase("nü4", "nǜ")] 
        #endregion
        public void ShouldConvertSingleVowels(string input, string expected)
        {
            AssertConversion(input, expected);
        }

        #endregion Public Methods

        #region Private Static Methods

        private static void AssertConversion(string input, string expected)
        {
            var converter = new PinyinBeautifier();

            var result = converter.Beautify(input);

            Assert.AreEqual(expected, result);
        }

        #endregion Private Static Methods
    }
}