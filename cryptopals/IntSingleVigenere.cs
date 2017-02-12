using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptopals
{
    public class IntSingleVigenere
    {
        static double[] lFreqs = {
             0.08167,
             0.01492,
             0.02782,
             0.04253,
             0.12702,
             0.02228,
             0.02015,
             0.06094,
             0.06966,
             0.00153,
             0.00772,
             0.04025,
             0.02406,
             0.06749,
             0.07507,
             0.01929,
             0.00095,
             0.05987,
             0.06327,
             0.09056,
             0.02758,
             0.00978,
             0.0236,
             0.0015,
             0.01974,
             0.00074,
             0.14,
             0.001
        };

        const int punishFactor = 1000000;
        const int puncFactor = 500;
        const int numFactor = 1000;
        const int weirdFactor = 20000;
        const int whitespFactor = 200;


        public static string decipherCText(string hexText, char key)
        {
            int[] cText = IntConversion.hexToInts(hexText);

            int[] plainText = new int[cText.Length];

            for(int i = 0; i < plainText.Length; i++)
            {
                plainText[i] = cText[i] ^ key;
            }

            return IntConversion.intsToString(plainText);
        }

        public static double CaesarL2NormSquared(string s)
        {
            double norm = 0;

            int[] actualFreqs = new int[26];
            int superPunish = 0;
            int punc = 0;
            int num = 0;
            int weird = 0;
            int whitespace = 0;

            string ignore = " .,'";

            for (int i = 0; i < 26; i++) { actualFreqs[i] = 0; }

            for (int i = 0; i < s.Length; i++)
            {
                int index = 27;
                int c = s[i];
                if (!(ignore.Contains((char)c)))
                {
                    if (char.IsLower((char)c)) index = c - 97;
                    else if (char.IsUpper((char)c)) index = c - 65;
                    else if (char.IsNumber((char)c)) num++;
                    else if (char.IsPunctuation((char)c)) punc++;
                    else if (char.IsWhiteSpace((char)c)) whitespace++;
                    else superPunish++;
                    
                    if (index < 26) actualFreqs[index] += 1;
                }
            }

            try
            {
                for (int i = 0; i < 26; i++)
                {
                    //actualFreqs[i] /= s.Length;
                    norm += Math.Abs((actualFreqs[i] - (lFreqs[i] * s.Length)) * (actualFreqs[i] - (lFreqs[i] * s.Length)));
                }

                norm += punishFactor * superPunish; //punish weird characters
                norm += puncFactor * punc;
                norm += numFactor * num;
                norm += punishFactor * weird;
                norm += whitespFactor * whitespace;
            }
            catch { norm = -1.0; }



            return norm;
        }

        public static Tuple<double, char, string> decipherCaesar(string cText)
        {

            double minNorm = -1.0;
            char minChar = 'a';
            string minString = "";

            for (int c = 0; c < 256; c++)
            {


                string tempString = decipherCText(cText, (char)c);
                double tempNorm = CaesarL2NormSquared(tempString);

                if (((minNorm < 0) && (tempNorm < punishFactor)) || ((minNorm >= 0) && (tempNorm < minNorm) && (tempNorm > 0)))
                {
                    minNorm = tempNorm;
                    minChar = (char)c;
                    minString = tempString;
                }

                //if (tempNorm < punishFactor) Console.WriteLine((int)c + ", " + tempNorm + " , " + tempString);

            }

            return new Tuple<double, char, string>(minNorm, minChar, minString);
        }
    }
}
