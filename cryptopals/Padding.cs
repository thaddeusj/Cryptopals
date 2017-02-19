using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptopals
{
    public static class Padding
    {
        public static byte[] PKCS7Pad(byte[] unpadded, int length)
        {
            if ((length - unpadded.Length) > 256) throw new Exception("Block too long");

            byte[] padded = new byte[length];

            byte pad = (byte)(length - unpadded.Length);

            for (int i = 0; i < unpadded.Length; i++) { padded[i] = unpadded[i]; }
            for (int i = unpadded.Length; i < length; i++) { padded[i] = pad; }


            return padded;
        }

        //Pad so that array is in blocks of blockLength
        public static byte[] PKCS7PadToBlockLength(byte[] unpadded, int blockLength)
        {
            int length = unpadded.Length + blockLength - (unpadded.Length % blockLength);

            return PKCS7Pad(unpadded, length);

        }
        
    }
}
