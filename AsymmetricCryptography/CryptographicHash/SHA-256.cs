using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography.CryptographicHash
{
    class SHA_256:CryptographicHashAlgorithm
    {
        //Таблица констант (первые 32 бита дробных частей кубических корней первых 64 простых чисел [от 2 до 311])
        private static readonly UInt32[] k = new UInt32[]
        {
            0x428A2F98, 0x71374491, 0xB5C0FBCF, 0xE9B5DBA5, 0x3956C25B, 0x59F111F1, 0x923F82A4, 0xAB1C5ED5,
            0xD807AA98, 0x12835B01, 0x243185BE, 0x550C7DC3, 0x72BE5D74, 0x80DEB1FE, 0x9BDC06A7, 0xC19BF174,
            0xE49B69C1, 0xEFBE4786, 0x0FC19DC6, 0x240CA1CC, 0x2DE92C6F, 0x4A7484AA, 0x5CB0A9DC, 0x76F988DA,
            0x983E5152, 0xA831C66D, 0xB00327C8, 0xBF597FC7, 0xC6E00BF3, 0xD5A79147, 0x06CA6351, 0x14292967,
            0x27B70A85, 0x2E1B2138, 0x4D2C6DFC, 0x53380D13, 0x650A7354, 0x766A0ABB, 0x81C2C92E, 0x92722C85,
            0xA2BFE8A1, 0xA81A664B, 0xC24B8B70, 0xC76C51A3, 0xD192E819, 0xD6990624, 0xF40E3585, 0x106AA070,
            0x19A4C116, 0x1E376C08, 0x2748774C, 0x34B0BCB5, 0x391C0CB3, 0x4ED8AA4A, 0x5B9CCA4F, 0x682E6FF3,
            0x748F82EE, 0x78A5636F, 0x84C87814, 0x8CC70208, 0x90BEFFFA, 0xA4506CEB, 0xBEF9A3F7, 0xC67178F2
        };

        private UInt32[] Hash;

        private byte[] preprocessedMessage;

        public override int GetDigestBitLength()
        {
            return 256;
        }

        public override byte[] GetHash(byte[] message)
        {
            //инициализация хеша
            //первые 32 бита дробных частей квадратных корней первых восьми простых чисел [от 2 до 19]
            Hash = new UInt32[]
            {
                0x6A09E667,0xBB67AE85,0x3C6EF372,0xA54FF53A, 0x510E527F,0x9B05688C,0x1F83D9AB,0x5BE0CD19
            };

            Preprocessing(message);
            for (int i = 0; i < preprocessedMessage.Length; i++)
            {
                Console.WriteLine(Convert.ToString(preprocessedMessage[i], 2));
            }
            //разделение на блоки и работа с каждым
            for (int index = 0; index < preprocessedMessage.Length; index += 64)
            {
                //берётся блок
                byte[] block = new byte[64];

                for (int j = 0; j < 64; j++)
                {
                    block[index] = preprocessedMessage[index + j];
                }

                //создаётся 64 слова длиной 32 бит.
                //первые 16 слов берутся из обрабатываемого блока (каждые 4 байта преобразуются в одно слово)
                UInt32[] processingBlock = new UInt32[64];

                //создание слов 
                for (int i = 0; i < 16; i++)
                {
                    processingBlock[i] = 0;

                    //каждые 4 байта из блока преобразуются в 32-х битный формат
                    for (int j = 0; j < 4; j++)
                    {
                        processingBlock[i] = (processingBlock[i] << 8) | block[i * 4 + j];
                    }
                }

                //остальные слова вычисляются по формулам
                //дальше весь алгоритм до конца цикла идёт из официальной документации
                for (int i = 16; i < 64; i++)
                {
                    processingBlock[i] = SmallSigma1(processingBlock[i - 2]) + processingBlock[i - 7] + SmallSigma0(processingBlock[i - 15]) + processingBlock[i - 16];
                }

                UInt32 a = Hash[0];
                UInt32 b = Hash[1];
                UInt32 c = Hash[2];
                UInt32 d = Hash[3];
                UInt32 e = Hash[4];
                UInt32 f = Hash[5];
                UInt32 g = Hash[6];
                UInt32 h = Hash[7];

                for (int i = 0; i < 64; i++)
                {
                    UInt32 t1 = h + BigSigma1(e) + Ch(e, f, g) + k[i] + processingBlock[i];
                    UInt32 t2 = BigSigma0(a) + Maj(a, b, c);

                    h = g;
                    g = f;
                    f = e;
                    e = d + t1;
                    d = c;
                    c = b;
                    b = a;
                    a = t1 + t2;
                }

                Hash[0] = a + Hash[0];
                Hash[1] = b + Hash[1];
                Hash[2] = c + Hash[2];
                Hash[3] = d + Hash[3];
                Hash[4] = e + Hash[4];
                Hash[5] = f + Hash[5];
                Hash[6] = g + Hash[6];
                Hash[7] = h + Hash[7];
            }

            //дайджест (результирующий хеш) будет хранится в массиве байтов
            byte[] digest = new byte[32];

            //перевод из little endian в big endian и запись в массив
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    digest[i * 4 + 3 - j] = Convert.ToByte(Hash[i] % 256);

                    Hash[i] >>= 8;
                }
            }

            return digest;
        }

        //предобработка данных
        private void Preprocessing(byte[] message)
        {
            int messageBytesCount = message.Length;

            int OffsetEmptyBytesCount;

            //вычисление количества пустых байтов для заполнения
            if (BLOCK_SIZE - ((messageBytesCount % BLOCK_SIZE) + FIRST_OFFSET_BYTE_SIZE) < LENGTH_SIZE)
            {
                //если в последний блок нехватает места для записи длины, то 
                //в последний блок дописываются нули, затем создаётся новый блок
                //заполняется нулями, кроме последних 8 байтов (для записи длины)
                OffsetEmptyBytesCount = (BLOCK_SIZE - messageBytesCount % BLOCK_SIZE) - FIRST_OFFSET_BYTE_SIZE;

                OffsetEmptyBytesCount += 56;
            }
            else
            {
                //если в последний блок можно вписать длину и маркер
                OffsetEmptyBytesCount = BLOCK_SIZE - FIRST_OFFSET_BYTE_SIZE - LENGTH_SIZE - messageBytesCount % BLOCK_SIZE;
            }

            int preprocessedMessageBytesCount = messageBytesCount + FIRST_OFFSET_BYTE_SIZE + OffsetEmptyBytesCount + LENGTH_SIZE;

            //новый массив байтов, в котором будут предобработанные данные
            preprocessedMessage = new byte[preprocessedMessageBytesCount];

            //переписываение данных из сообдения
            for (int i = 0; i < messageBytesCount; i++)
            {
                preprocessedMessage[i] = message[i];
            }

            //ставится маркер
            preprocessedMessage[messageBytesCount] = OffsetFirstByte;

            //после маркера ставятся пустые байты до тех пор, пока не останется 8 свободных байт
            for (int i = 0; i < OffsetEmptyBytesCount; i++)
            {
                preprocessedMessage[i + messageBytesCount + 1] = OffsetEmptyByte;
            }

            //количество бит в сообщении хранится в 8 байтном беззначном целочисленном типе
            UInt64 messageCountBits = Convert.ToUInt64(messageBytesCount * 8);

            //запись количества бит сообщения
            for (int i = LENGTH_SIZE - 1; i >= 0; i--)
            {
                preprocessedMessage[i + messageBytesCount + 1 + OffsetEmptyBytesCount] = Convert.ToByte(messageCountBits % 256);

                messageCountBits >>= 8;
            }
        }

        //ниже идут формулы из документации
        private UInt32 Ch(UInt32 x, UInt32 y, UInt32 z)
        {
            return (x & y) ^ (~x & z);
        }

        private UInt32 Maj(UInt32 x, UInt32 y, UInt32 z)
        {
            return (x & y) ^ (x & z) ^ (y & z);
        }

        private UInt32 BigSigma0(UInt32 x)
        {
            return RotateRight(x, 2) ^ RotateRight(x, 13) ^ RotateRight(x, 22);
        }

        private UInt32 BigSigma1(UInt32 x)
        {
            return RotateRight(x, 6) ^ RotateRight(x, 11) ^ RotateRight(x, 25);
        }

        private UInt32 SmallSigma0(UInt32 x)
        {
            return RotateRight(x, 7) ^ RotateRight(x, 18) ^ (x >> 3);
        }

        private UInt32 SmallSigma1(UInt32 x)
        {
            return RotateRight(x, 17) ^ RotateRight(x, 19) ^ (x >> 10);
        }
    }
}
