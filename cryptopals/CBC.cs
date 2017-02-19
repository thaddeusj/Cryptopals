using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace cryptopals
{
    public static class CBC
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pText">Does not need to be pre-padded.</param>
        /// <param name="key"></param>
        /// <param name="blockLength"></param>
        /// <param name="randIV">False sets the IV to 0.</param>
        /// <returns></returns>
        public static byte[] aesCBC(byte[] pText, byte[] key,int blockLength, bool randIV)
        {
            byte[] padPText = Padding.PKCS7PadToBlockLength(pText, blockLength);
            byte[] cText = new byte[padPText.Length];

            byte[] IV = new byte[blockLength];
            if(randIV == true)
            {
                (new Random()).NextBytes(IV);
            }
            else
            {
                for (int i = 0; i < IV.Length; i++) IV[i] = 0;
            }

            byte[] curNonce = IV;

            AesManaged alg = new AesManaged { KeySize = blockLength*8, Key = key, BlockSize = blockLength*8,
                                              Mode = CipherMode.ECB, Padding = PaddingMode.Zeros, IV = curNonce };
            
            
            byte[] pBlock = new byte[blockLength];
            byte[] cBlock = new byte[blockLength];

            for(int i = 0; blockLength*i < padPText.Length; i++)
            {
                for(int j = 0; j < blockLength; j++)
                {
                    pBlock[j] = padPText[blockLength * i + j];
                }

                ICryptoTransform enc = alg.CreateEncryptor(key, curNonce);
                enc.TransformBlock(pBlock, 0, blockLength, cBlock, 0);

                for(int j = 0; i < blockLength; i++)
                {
                    curNonce[j] = cBlock[j];
                    cText[blockLength * i + j] = cBlock[j];
                }

                alg.IV = curNonce;
            }

            return cText;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dec">Decryptor transform for your algorithm. InputBlockSize must be set to your block size. IV must be set to 0.</param>
        /// <param name="cText"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static byte[] CBCdec(ICryptoTransform dec, byte[] cText, byte[] IV)
        {
            byte[] pText = new byte[cText.Length];

            int blockSize = dec.InputBlockSize;

            dec.TransformBlock(cText, 0, cText.Length, pText, 0);

            
            for(int i = cText.Length - 1; i >= blockSize; i--)
            {
                pText[i] = (byte)(pText[i] ^ cText[i - blockSize]);

            }
            for(int i = 0; i < blockSize; i++) { pText[i] = (byte)(pText[i] ^ IV[i]); }


            return pText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enc">InputBlockSize must be set to your block size, and IV set to 0.</param>
        /// <param name="pText"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static byte[] CBCenc(ICryptoTransform enc, byte[] pText, byte[] IV)
        {
            byte[] padPText = Padding.PKCS7PadToBlockLength(pText, enc.InputBlockSize);
            byte[] cText = new byte[padPText.Length];

            byte[] curNonce = new byte[IV.Length]; // Be careful! We change curNonce, so we have to initializa the hard way.
            for(int i = 0;i < IV.Length; i++) { curNonce[i] = IV[i]; }


            int blockLength = enc.InputBlockSize;
            
            byte[] pBlock = new byte[blockLength];
            byte[] cBlock = new byte[blockLength];

            

            for (int i = 0; blockLength * i < padPText.Length; i++)
            {
                for (int j = 0; j < blockLength; j++)
                {
                    pBlock[j] = (byte)(padPText[blockLength * i + j]^curNonce[j]);

                }
                
                enc.TransformBlock(pBlock, 0, blockLength, cBlock, 0);

                for (int j = 0; j < blockLength; j++)
                {
                    curNonce[j] = cBlock[j];
                    cText[blockLength * i + j] = cBlock[j];
                }
                
            }



            return cText;
        }



    }
}
