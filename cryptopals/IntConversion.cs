using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptopals
{
    public class IntConversion
    {
        //replace bit array with int arrays

        public const string hexMap = "0123456789abcdef"; //hex alphabet


        public static int[] hexToInts(string hex)
        {
            int[] charAr = new int[hex.Length / 2];

            for (int i = 0; 2 * i < hex.Length; i++)
            {
                charAr[i] = 16 * hexMap.LastIndexOf(hex[2 * i]) + hexMap.LastIndexOf(hex[2 * i + 1]);
            }


            return charAr;
        }

        //public static int[] intKey(char k, int bytes)
        //{
        //    int[] key = new int[bytes];
        //    for(int i = 0; i < bytes; i++) { key[i] = k; }

        //    return key;

        //}

        public static string intsToString(int[] bytes)
        {
            string s = "";

            for (int i = 0; i < bytes.Length; i++) { s = s + ((char)bytes[i]); }

            return s;
        }

        public static string intsToHex(int[] bytes)
        {
            string hex = "";

            for(int i = 0; i < bytes.Length; i++)
            {
                hex = hex + hexMap[(bytes[i] / 16) % 16];
                hex = hex + hexMap[bytes[i] % 16];
            }

            return hex;
        }

        

    }
}
