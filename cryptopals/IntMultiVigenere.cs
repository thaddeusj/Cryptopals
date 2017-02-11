using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptopals
{
    public class IntMultiVigenere
    {



        public static string decipherCText(string Text, char[] key)
        {
            int[] cText = new int[Text.Length];
            
            for(int i = 0; i < Text.Length; i++)
            {
                cText[i] = Text[i];
            }

            int[] plainText = new int[cText.Length];

            if (key.Length == 0) throw new Exception("Empty key");

            for (int i = 0; i <= plainText.Length / key.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if ((key.Length * i + j) < cText.Length)
                    {
                        int xor = cText[key.Length * i + j] ^ key[j];
                        plainText[key.Length * i + j] = xor;
                    }
                }

            }

            return IntConversion.intsToHex(plainText);
        }

        public static string decipherCTextHex(string hexText, char[] key)
        {
            int[] cText = IntConversion.hexToInts(hexText);

            int[] plainText = new int[cText.Length];

            if (key.Length == 0) throw new Exception("Empty key");
            if (hexText.Length % key.Length != 0) throw new Exception("Key doesn't fit");

            for (int i = 0; i < plainText.Length / key.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    plainText[i] = cText[key.Length*i + j] ^ key[j];
                } 
                
            }

            return IntConversion.intsToString(plainText);
        }





        //Hamming dist
        public static int hammingDist(string s1, string s2)
        {
            int dist = 0;


            int[] s1Ints = IntConversion.hexToInts(s1);
            int[] s2Ints = IntConversion.hexToInts(s2);


            if (s1Ints.Length != s2Ints.Length) throw new Exception("Different lengths");

            for (int i = 0; i < s1Ints.Length; i++)
            {
                int temp1 = s1Ints[i];
                int temp2 = s2Ints[i];


                for (int j = 0; j < 8; j++)
                {
                    if ((temp1 % 2) != (temp2 % 2)) dist++;

                    temp1 /= 2;
                    temp2 /= 2;
                }
            }
            

            return dist;
        }





    }
}
