using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cryptopals
{
    class Set1
    {
        public static void Challenge1()
        {

            const string hex = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";

            Console.WriteLine(StringConverters.BitsToBase64(StringConverters.HexToBits(hex)));


        }

        public static void Challenge2()
        {
            const string plaintext = "1c0111001f010100061a024b53535009181c";
            const string cipher = "686974207468652062756c6c277320657965";

            Console.WriteLine(StringConverters.BitsToHex(StringConverters.HexToBits(plaintext).Xor(StringConverters.HexToBits(cipher))));
        }

        public static void Challenge3()
        {
            string ciphertext = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
           

            Console.WriteLine(CaesarTools.decipherCaesar(ciphertext));

        }

        public static void Challenge4()
        {
            StreamReader challengeReader = File.OpenText(@"C:\Users\User\documents\visual studio 2015\Projects\cryptopals\cryptopals\set 1 challenge 4.txt");

            List<string> chalStrings = new List<string>();

            string temp;
            string min = "";
            double minNorm = -1.0;

            while((temp = challengeReader.ReadLine())!= null)
            {
                chalStrings.Add(temp);
            }

            challengeReader.Close();

            for (int i = 0; i < chalStrings.Count; i++)
            {
                string s = chalStrings[i];

                Tuple<double, char, string> tempT = CaesarTools.decipherCaesar(s);


                if (((tempT.Item1 < minNorm) || (minNorm < 0)) && (tempT.Item1 > 0))
                {
                    minNorm = tempT.Item1;
                    min = tempT.Item3;
                }

                Console.WriteLine(i + ", " + tempT.Item3 + ":   " + tempT.Item1);

                //Console.ReadLine();
                //Console.Clear();

            }

            //Tuple<double, char, string> tempT = CaesarTools.decipherCaesar(chalStrings[195]);
            //tempT = CaesarTools.decipherCaesar(chalStrings[225]);
            //tempT = CaesarTools.decipherCaesar(chalStrings[230]);
            //tempT = CaesarTools.decipherCaesar(chalStrings[295]);


            Console.WriteLine(min + ", " + minNorm);

        }
    }
}
