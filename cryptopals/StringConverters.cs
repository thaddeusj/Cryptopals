using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace cryptopals
{
    public class StringConverters
    {
        public const string b64Map = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"; //Base64 Alphabet
        public const string hexMap = "0123456789abcdef"; //hex alphabet


        //Is the string valid hex?
        public static bool ValidateHex(string hex)
        {
            foreach (char n in hex)
            {
                if (!(hexMap.Contains(n))) { return false; }

            }
            return true;
        }


        //Converts a hex string to a BitArray
        public static BitArray HexToBits(string hex)
        {
            BitArray bits = new BitArray(4 * hex.Length);

            for (int i = 0; i < hex.Length; i++)
            {
                int hexchar = hexMap.LastIndexOf(hex[i]);


                if (hexchar < 0) throw new FormatException("Validation failed. Ugh.");

                bits[4 * i + 3] = ((hexchar % 2) == 1) ? true : false;
                hexchar = hexchar >> 1;
                bits[4 * i + 2] = ((hexchar % 2) == 1) ? true : false;
                hexchar = hexchar >> 1;
                bits[4 * i + 1] = ((hexchar % 2) == 1) ? true : false;
                hexchar = hexchar >> 1;
                bits[4 * i] = ((hexchar % 2) == 1) ? true : false;

            }

            return bits;
        }


        //Converts a BitArray, representing a number, to a hex string
        public static string BitsToHex(BitArray bits)
        {
            string hex = "";

            if (bits.Length % 4 != 0) throw new Exception("No hex representation: bits don't fit.");

            for (int i = 0; 4 * i < bits.Length; i++)
            {

                int index = 0;
                for (int j = 0; j < 4; j++)
                {
                    index *= 2;
                    if (bits[4 * i + j] == true) index++;
                }


                hex = hex + hexMap[index];

            }

            return hex;
        }



        //Validate base 64 string
        public static bool ValidateBase64(string b64)
        {
            foreach (char c in b64) { if (!(b64Map.Contains(c))) return false; }
            return true;
        }

        //Convert a number represented in base 64 to bits
        public static BitArray Base64ToBits(string b64)
        {
            BitArray bits = new BitArray(b64.Length * 6);

            ValidateBase64(b64);

            for (int i = 0; i < b64.Length; i++)
            {
                int index = b64Map.LastIndexOf(b64[i]);

                for (int j = 5; j >= 0; j--)
                {
                    bits[6 * i + j] = (index % 2 == 1) ? true : false;

                    index = index >> 1;
                }

            }

            return bits;
        }



        //Converts a BitArray, which represents some number,
        //to a string containing the base 64 representation of the
        public static string BitsToBase64(BitArray bits)
        {
            string base64 = "";

            if (bits.Length % 6 != 0) throw new Exception("No base 64 representation: bits don't fit.");

            for (int i = 0; 6 * i < bits.Length; i++)
            {

                int index = 0;
                for (int j = 0; j < 6; j++)
                {
                    index *= 2;
                    if (bits[6 * i + j] == true) index++;
                }


                base64 = base64 + b64Map[index];

            }

            return base64;
        }

        //Converts a string into a BitArray, character by character
        //public static BitArray StringToBits (string s)
        //{
        //    int[] sChars = new int[s.Length];
        //    for(int i = 0; i < s.Length; i++)
        //    {
        //        sChars[i] = s[i];
        //    }

        //    return new BitArray(sChars);

        //}

        //Convert a BitArray to a string, byte by byte
        public static string BitsToString (BitArray bits)
        {
            string s = "";

            for (int i = 0; 8 * i < bits.Length; i++)
            {
                               

                int index = 0;
                for (int j = 0; j < 8; j++)
                {
                    index *= 2;
                    if (bits[8 * i + j] == true) index++;
                }


                s = s + (char)index;

            }

            return s;

        }


        public static BitArray CharToBits(char c, int bytes)
        {
            BitArray bits = new BitArray(8 * bytes);

            int cInt = (int)c;
            BitArray cBits = new BitArray(8);

            for (int j = 7; j >= 0; j--)
            {
                cBits[j] = ((cInt % 2) == 1) ? true : false;
                cInt = cInt >> 1;
            }

            for (int i = 0; i < bytes; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bits[8 * i + j] = cBits[j];
                }
            }

            return bits;

        }
    }
}
    
