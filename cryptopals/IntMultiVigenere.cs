using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptopals
{
    public class IntMultiVigenere
    {

        public static int findKeySize(int[] bytes)
        {
            int keySize = 0;
            double minD = -1.0;
            
            for(int k = 2; k <= 40; k++)
            {
                int bsNum = 25;

                int[][] blocks = new int[2*bsNum][];


                for (int j = 0; j < 2*bsNum; j++)
                {
                    blocks[j] = new int[k];

                    for (int i = 0; i < k; i++)
                    {

                        blocks[j][i] = bytes[i + j*k];
                    }
                }
                
                double tempD = 0.0;

                for(int i = 0; i < bsNum; i++)
                {
                    double d = ((double)ihammingDist(blocks[2 * i], blocks[2 * i + 1])) / k;
                    tempD = tempD + d;
                }

                //Console.WriteLine("Keysize = " + k + String.Format("{0,20}", tempD));

                if ((minD < 0) || (tempD < minD))
                {
                    minD = tempD;
                    keySize = k;
                }

                int stop = 0;
            }

            return keySize;
        }

        public static List<int[]> transposeBlocks(int[] bytes, int blocksize)
        {
            List<int[]> newBs = new List<int[]>();

            for(int i = 0; i < blocksize; i++)
            {
                int thisBSize = ((bytes.Length % blocksize > i)) ? bytes.Length / blocksize + 1 : bytes.Length / blocksize;
                
                int[] curBlock = new int[thisBSize];

                for(int j = 0; j*blocksize + i < bytes.Length; j++)
                {
                    curBlock[j] = bytes[blocksize * j + i];
                }

                newBs.Add(curBlock);
            }


            return newBs;

        }

        public static int[] undoTranspose(List<int[]> blocks)
        {
            
            int totalBytes = 0;

            foreach(int[] block in blocks) { totalBytes += block.Length; }

            int[] newBs = new int[totalBytes];

            for (int i = 0; i < blocks.Count; i++)
            {
                for(int j = 0; j < blocks[i].Length; j++)
                {
                    newBs[j * blocks.Count + i] = blocks[i][j];
                }
            }


            return newBs;

        }

        public static string crackMV(string hex)
        {
            string pText = "";

            int[] hexBytes = IntConversion.hexToInts(hex);

            int likelyKSize = findKeySize(hexBytes);
           
            List<int[]> transposedBlocks = transposeBlocks(hexBytes, likelyKSize);
            List<int[]> decipheredBlocks = new List<int[]>();

            for (int i = 0; i < transposedBlocks.Count; i++)
            {
                decipheredBlocks.Add(StringConverters.stringToInts(IntSingleVigenere.decipherCaesar(IntConversion.intsToHex(transposedBlocks[i])).Item3));
            }

            pText = IntConversion.intsToString(undoTranspose(decipheredBlocks));
            //Console.WriteLine(likelyKSize + " : " + pText);
            
            return pText;
        }

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


            int[] s1Ints = StringConverters.stringToInts(s1);
            int[] s2Ints = StringConverters.stringToInts(s2);


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

        public static int ihammingDist(int[] s1, int[] s2)
        {
            int dist = 0;
            
            if (s1.Length != s2.Length) throw new Exception("Different lengths");

            for (int i = 0; i < s1.Length; i++)
            {
                int temp1 = s1[i];
                int temp2 = s2[i];


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
