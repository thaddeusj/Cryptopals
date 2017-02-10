using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace cryptopals
{
    public class CaesarTools
    {
        //Tools for recongnizing Caesar ciphers

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

        public static double CaesarL2NormSquared(string s)
        {
            double norm = 0;

            int[] actualFreqs = new int[26];
            int superPunish = 0;
            int punc = 0;
            int num = 0;
            int weird = 0;

            string ignore = " .,@'";

            for(int i = 0; i < 26; i++){ actualFreqs[i] = 0; }

            for(int i = 0; i < s.Length; i++)
            {
                int index = 27;
                int c = s[i];
                if (!(ignore.Contains((char)c)))
                {
                    if ((c >= 97) && (c <= 122)) index = c - 97;
                    if ((c >= 65) && (c <= 90)) index = c - 65;

                    if (index < 26) actualFreqs[index] += 1;

                    if (c < 32) superPunish += 1;
                    if (c > 126)
                    {
                        if (c == 127) superPunish++;
                        if (c > 165) superPunish++;
                        else weird++;
                    }
                    if (((c > 32) && (c < 65) && ((c < 48) || (c > 57))) || ((c <= 126) && (c >= 123)) || ((c >= 90) && (c <= 96))) punc += 1;
                    if ((c >= 48) && (c <= 57)) num++;
                }
            }

            try
            {
                for (int i = 0; i < 26; i++)
                {
                    //actualFreqs[i] /= s.Length;
                    norm += Math.Abs((actualFreqs[i] - lFreqs[i]*s.Length)* (actualFreqs[i] - lFreqs[i]*s.Length));
                }

                norm += punishFactor*superPunish; //punish weird characters
                norm += puncFactor * punc;
                norm += numFactor * num;
                norm += punishFactor * weird;
            }
            catch { norm = -1.0; }



            return norm;
        }

        public static Tuple<double, char,string> decipherCaesar(string cText)
        {

            double minNorm = -1.0;
            char minChar = 'a';
            string minString = "";
            
            for (int c = 0; c < 256; c++)
            {
                
                char d = (char)c;

                string tempString = decipherCText(cText, d);
                double tempNorm = CaesarL2NormSquared(tempString);
                if (((minNorm < 0) && (tempNorm < punishFactor )) || ((minNorm >= 0) && (tempNorm < minNorm) && (tempNorm > 0)))
                {
                    minNorm = tempNorm;
                    minChar = (char)c;
                    minString = tempString;
                }

                if(tempNorm < punishFactor) Console.WriteLine((int)c +  ", " + tempNorm + " , " + tempString);

            }

            return new Tuple<double, char, string>(minNorm, minChar,minString);
        }

        public static string decipherCText(string cText, char cipher)
        {
            BitArray cTextBits = StringConverters.HexToBits(cText);

            BitArray cipherBits = StringConverters.CharToBits(cipher, cText.Length/2);

            BitArray xor = cTextBits.Xor(cipherBits);


            return StringConverters.BitsToString(xor);
        }

    }
}
