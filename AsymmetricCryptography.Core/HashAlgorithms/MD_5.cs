using AsymmetricCryptography.DataUnits;

namespace AsymmetricCryptography.Core.HashAlgorithms
{
    /// <summary>
    /// Cryptographic hash algorithm MD-5
    /// </summary>
    public sealed class MD_5 : HashAlgorithm
    {
        public override int DigestBitSize => 128;

        public MD_5()
            :base(CryptographicHashAlgorithm.MD_5) { }

        /// <summary>
        /// S specifies the per-round shift amounts
        /// </summary>
        private static readonly byte[] S = new byte[]
        {
            7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,
            5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,
            4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,
            6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21
        };

        /// <summary>
        /// Binary integer part of the sines of integers (Radians) as constants
        /// </summary>
        private static readonly UInt32[] K = new UInt32[]
        {
            0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
            0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
            0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
            0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
            0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
            0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
            0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
            0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
            0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
            0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
            0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05,
            0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
            0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
            0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
            0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
            0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
        };

        private const int BLOCK_SIZE = 64;
        private const int MESSAGE_SIZE_BYTE_LENGTH = 8;
        private const int MARKER_BYTE_LENGTH = 1;

        /// <summary>
        /// End of data marker
        /// </summary>
        private const byte MARKER = 0x80;

        private UInt32[] Hash;

        public override byte[] GetHash(byte[] data)
        {
            Hash = new UInt32[] { 0x67452301, 0xefcdab89, 0x98badcfe, 0x10325476 };

            int dataLength = data.Length;

            var message = Preprocess(data, BLOCK_SIZE, MARKER_BYTE_LENGTH, MESSAGE_SIZE_BYTE_LENGTH);

            // вставка маркера, сообщающего о конце сообщения
            message[dataLength] = MARKER;

            // битовая величина размера сообщения
            byte[] sizeBytes = BitConverter.GetBytes(Convert.ToUInt64(dataLength * 8));

            // lowOrder - младшие к байта, highOrder - старшие 4 байта
            byte[] lowOrder = new byte[4];
            byte[] highOrder = new byte[4];

            // вычисление ордеров
            for (int i = 0; i < 4; i++)
            {
                highOrder[i] = sizeBytes[i];
                lowOrder[i] = sizeBytes[i + 4];
            }

            // запись длины сообщения с поменяными местами lowOrder и highOrder
            for (int i = message.Length - 1, orderIndex = 3; i >= message.Length - 4; i--, orderIndex--) 
            {
                message[i] = lowOrder[orderIndex];
                message[i - 4] = highOrder[orderIndex];
            }

            //разделение на блоки и работа с каждым
            for (int index = 0; index < message.Length; index += 64)
            {
                //берётся блок
                byte[] currentBlock = new byte[64];

                for (int j = 0; j < 64; j++)
                {
                    currentBlock[j] = message[index + j];
                }

                //создаётся 16 слова длиной 32 бит(каждые 4 байта преобразуются в одно слово)
                UInt32[] processingBlock = new UInt32[16];

                // создание слов из байтов
                for (int i = 0; i < 16; i++)
                {
                    //каждые 4 байта из блока преобразуются в 32-х битный формат
                    processingBlock[i] = 0;

                    // запись идёт сразу в little endian
                    for (int j = 0; j < 4; j++)
                    {
                        processingBlock[i] = (processingBlock[i] << 8) | currentBlock[i * 4 + 3 - j];
                    }
                }

                //дальше всё идёт по алгоритму
                UInt32 A = Hash[0];
                UInt32 B = Hash[1];
                UInt32 C = Hash[2];
                UInt32 D = Hash[3];

                for (UInt32 i = 0; i < 64; i++)
                {
                    UInt32 f = 0, g = 0;

                    if (i >= 0 && i <= 15)
                    {
                        f = F(B, C, D);
                        g = i;
                    }
                    else if (i >= 16 && i <= 31)
                    {
                        f = G(B, C, D);
                        g = (5 * i + 1) % 16;
                    }
                    else if (i >= 32 && i <= 47)
                    {
                        f = H(B, C, D);
                        g = (3 * i + 5) % 16;
                    }
                    else if (i >= 48 && i <= 63)
                    {
                        f = I(B, C, D);
                        g = (7 * i) % 16;
                    }

                    f = f + A + K[i] + processingBlock[g];

                    A = D;
                    D = C;
                    C = B;
                    B = B + (RotateLeft(f, S[i]));
                }

                Hash[0] = Hash[0] + A;
                Hash[1] = Hash[1] + B;
                Hash[2] = Hash[2] + C;
                Hash[3] = Hash[3] + D;
            }

            //дайджест (результирующий хеш) будет хранится в массиве байтов
            byte[] digest = new byte[16];

            // запись в массив
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    digest[i * 4 + j] = Convert.ToByte(Hash[i] % 256);

                    Hash[i] >>= 8;
                }
            }

            return digest;
        }

        //функции из документации
        private UInt32 F(UInt32 x, UInt32 y, UInt32 z)
        {
            return (x & y) | (~x & z);
        }

        private UInt32 G(UInt32 x, UInt32 y, UInt32 z)
        {
            return (x & z) | (~z & y);
        }

        private UInt32 H(UInt32 x, UInt32 y, UInt32 z)
        {
            return x ^ y ^ z;
        }

        private UInt32 I(UInt32 x, UInt32 y, UInt32 z)
        {
            return y ^ (x | (~z));
        }
    }
}
