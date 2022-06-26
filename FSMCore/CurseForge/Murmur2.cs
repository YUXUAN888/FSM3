using System.Collections.Generic;
using System.Text;

namespace CurseForge.APIClient
{
    internal class MurmurHash2
    {
        public static byte[] NormalizeByteArray(byte[] data)
        {
            var newArray = new List<byte>();

            foreach (var b in data)
            {
                if (!IsWhitespaceCharacter(b))
                {
                    newArray.Add(b);
                }
            }

            return newArray.ToArray();
        }

        private static bool IsWhitespaceCharacter(byte b)
        {
            return b == 9 || b == 10 || b == 13 || b == 32;
        }

        public static long Hash(string data)
        {
            return Hash(Encoding.UTF8.GetBytes(data));
        }

        public static long Hash(byte[] data)
        {
            return Hash(data, 1);
        }

        private const uint m = 0x5bd1e995;
        private const int r = 24;

        public static long Hash(byte[] data, uint seed)
        {
            var length = data.Length;
            if (length == 0)
            {
                return 0;
            }

            var h = seed ^ (uint)length;
            var currentIndex = 0;
            while (length >= 4)
            {
                var k = (uint)(data[currentIndex++] | data[currentIndex++] << 8 | data[currentIndex++] << 16 | data[currentIndex++] << 24);
                k *= m;
                k ^= k >> r;
                k *= m;

                h *= m;
                h ^= k;
                length -= 4;
            }
            switch (length)
            {
                case 3:
                    h ^= (ushort)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (uint)(data[currentIndex] << 16);
                    h *= m;
                    break;
                case 2:
                    h ^= (ushort)(data[currentIndex++] | data[currentIndex] << 8);
                    h *= m;
                    break;
                case 1:
                    h ^= data[currentIndex];
                    h *= m;
                    break;
                default:
                    break;
            }

            h ^= h >> 13;
            h *= m;
            h ^= h >> 15;

            return h;
        }
    }
}
