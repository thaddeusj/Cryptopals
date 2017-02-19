using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace cryptopals
{
    public class Set2
    {

        public static void Challenge2()
        {
            byte[] key = {(Byte)'Y', (Byte)'E', (Byte)'L', (Byte)'L', (Byte)'O', (Byte)'W', (Byte)' ', (Byte)'S', (Byte)'U',
                             (Byte)'B', (Byte)'M', (Byte)'A', (Byte)'R', (Byte)'I', (Byte)'N', (Byte)'E'};


            byte[] iv = {0,0,0,0,0,0,0,0,
                        0,0,0,0,0,0,0,0};

            StreamReader sr = new StreamReader(@"C:\Users\User\Documents\Visual Studio 2015\Projects\cryptopals\cryptopals\set 2 challenge 2.txt");
            string base64 = "";
            string temp = "";

            while ((temp = sr.ReadLine()) != null)
            {

                base64 = base64 + temp;

            }


            int[] iCText = IntConversion.hexToInts(StringConverters.BitsToHex(StringConverters.Base64ToBits(base64)));
            byte[] cText = new byte[iCText.Length];

            for (int i = 0; i < cText.Length; i++)
            {
                cText[i] = (byte)iCText[i];
            }

            AesManaged alg = new AesManaged { KeySize = 128, Key = key, BlockSize = 128, Mode = CipherMode.ECB, Padding = PaddingMode.Zeros, IV = iv };

            ICryptoTransform dec = alg.CreateDecryptor(key, iv);

            byte[] pText = CBC.CBCdec(dec, cText, iv);

            string sPText = "";

            for(int i = 0;i < pText.Length; i++) { sPText = sPText + (char)(pText[i]); }

            Console.WriteLine(sPText);

        }

        public static void Challenge2Enc()
        {
            string sPText = "Albert Einstein was a stone-cold mofo. Droppin' bombs, and spittin' fire.";
            byte[] key = {(Byte)'Y', (Byte)'E', (Byte)'L', (Byte)'L', (Byte)'O', (Byte)'W', (Byte)' ', (Byte)'S', (Byte)'U',
                             (Byte)'B', (Byte)'M', (Byte)'A', (Byte)'R', (Byte)'I', (Byte)'N', (Byte)'E'};
            byte[] pText = new byte[sPText.Length];
            for(int i = 0; i < pText.Length; i++)
            {
                pText[i] = (byte)sPText[i];
            }


            byte[] iv = {0,0,0,0,0,0,0,0,
                        0,0,0,0,0,0,0,0};

            //Note that our 


            AesManaged alg = new AesManaged { KeySize = 128, Key = key, BlockSize = 128, Mode = CipherMode.ECB, Padding = PaddingMode.Zeros, IV = iv };

            ICryptoTransform enc = alg.CreateEncryptor(key, iv);
            ICryptoTransform dec = alg.CreateDecryptor(key, iv);

            byte[] cText = CBC.CBCenc(enc, pText, iv);
            byte[] ppText = CBC.CBCdec(dec, cText, iv);

            

            bool isSame = true;
            
            for(int i = 0; i < pText.Length; i++)
            {
                if (ppText[i] != pText[i]) isSame = false;
            }

            Console.WriteLine("Does A.E. spit fire?    " + isSame);

        }





    }
}
