using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cryptopals;
using System.Collections;

namespace CryptopalsTests
{
    [TestClass]
    public class StringConverterTests
    {
        [TestMethod]
        public void ValidateHexTest()
        {
            Assert.IsTrue(StringConverters.ValidateHex("ab345"));
            Assert.IsTrue(StringConverters.ValidateHex("abcee23597862651456178965916efadccdafab1346"));

            Assert.IsFalse(StringConverters.ValidateHex("ab345y"));
            Assert.IsFalse(StringConverters.ValidateHex("5913489750ygigiguanbahbjkllnjkan"));

        }

        [TestMethod]
        public void ValidateBase64Test()
        {
            Assert.IsTrue(StringConverters.ValidateBase64("alkjgbaetvarbtalvun82haaebg78o54YSTWSJDYU"));
            Assert.IsFalse(StringConverters.ValidateBase64("alteruhlaulaegraerhlgaehrg."));
        }

        [TestMethod]
        public void HexToBitsTest()
        {
            BitArray[] hexBits = new BitArray[16];
            hexBits[0] = new BitArray(new bool[] { false, false, false, false });
            hexBits[1] = new BitArray(new bool[] { false, false, false, true });
            hexBits[2] = new BitArray(new bool[] { false, false, true, false });
            hexBits[3] = new BitArray(new bool[] { false, false, true , true });
            hexBits[4] = new BitArray(new bool[] { false, true, false, false });
            hexBits[5] = new BitArray(new bool[] { false, true, false, true });
            hexBits[6] = new BitArray(new bool[] { false, true, true, false });
            hexBits[7] = new BitArray(new bool[] { false, true, true, true });
            hexBits[8] = new BitArray(new bool[] { true, false, false, false });
            hexBits[9] = new BitArray(new bool[] { true, false, false, true });
            hexBits[10] = new BitArray(new bool[] { true, false, true, false });
            hexBits[11] = new BitArray(new bool[] { true, false, true, true });
            hexBits[12] = new BitArray(new bool[] { true, true, false, false });
            hexBits[13] = new BitArray(new bool[] { true, true, false, true });
            hexBits[14] = new BitArray(new bool[] { true, true, true, false });
            hexBits[15] = new BitArray(new bool[] { true, true, true, true });

            for(int i = 0; i < 16; i++)
            {
                string hexChar = StringConverters.hexMap[i].ToString();
                BitArray testBits = StringConverters.HexToBits(hexChar);

                for (int j = 0; j < 4; j++) { Assert.IsTrue(testBits[j] == hexBits[i][j]); }
            }
            

        }

        //[TestMethod]

        ////public void BitsToStringTest()
        //{
        //    char[] chars = new char[256];

        //    for(int i = 0; i <256; i++)
        //    {
        //        chars[i] = (char)i;
        //    }

        //    BitArray charBits = StringConverters.CharsToBits(chars);

        //    for(int i = 0; i < 256; i++)
        //    {
        //        BitArray tempBits = new BitArray(8);
        //        for(int j = 0; j < 8; j++) { tempBits[j] = charBits[8 * i + j]; }

        //        string s = StringConverters.BitsToString(tempBits);

        //        Assert.IsTrue((chars[i]) == s[0]);

        //    }

        //}

        [TestMethod]


        public void decodeTest()
        {
            //2 hex

            for (int c = 0; c < 256; c++)
            {
                for (int d = 0; d < 256; d++)
                {
                    string charString = Convert.ToByte(c).ToString("x");
                    if (charString.Length == 1) charString = "0" + charString;

                    string decChar = cryptopals.CaesarTools.decipherCText(charString, (char)d);

                    char expected = (char)(c ^ d);
                    Assert.IsTrue(decChar[0] == expected);
                }
            }

            //4 hex

            for (int c = 0; c < 256; c++)
            {
                for (int d = 0; d < 256; d++)
                {
                    for (int b = 0; b < 256; b++)
                    {
                        string charString1 = Convert.ToByte(c).ToString("x");
                        if (charString1.Length == 1) charString1 = "0" + charString1;
                        string charString2 = Convert.ToByte(b).ToString("x");
                        if (charString2.Length == 1) charString2 = "0" + charString2;

                        string cText = charString1 + charString2;

                        string decChar = cryptopals.CaesarTools.decipherCText(cText, (char)d);

                        char expected1 = (char)(c ^ d);
                        char expected2 = (char)(b ^ d);
                        string expected = "" + expected1 + expected2;
                        Assert.IsTrue(decChar.Equals(expected));
                    }
                }
            }
        }

        


    }
}
