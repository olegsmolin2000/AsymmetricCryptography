using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography
{
    static class BlockConverter
    {
        //функция нахождени количества байтов, которые можно обрабатывать по заданному модулю
        public static int GetBlockSize(BigInteger modulus)
        {
            const int BYTE_ELEMENTS_COUNT = 256;

            BigInteger byteSize = BYTE_ELEMENTS_COUNT;
            
            int size = 1;

            while (byteSize * BYTE_ELEMENTS_COUNT < modulus)
            {
                byteSize *= BYTE_ELEMENTS_COUNT;
                size++;
            }

            return size;
        }

        //функция для получения BigInt блоков из произвольного количества байтов
        public static BigInteger[] BytesToBlocks(byte[] message,int blockSize)
        {
            //нахождение количества будущих блоков
            int blocksCount = message.Length / blockSize;

            if (message.Length % blockSize != 0)
                blocksCount++;

            BigInteger[] blocks = new BigInteger[blocksCount];
            
            //внешний цикл даёт 1 блок на каждой итерации
            for (int i = 0; i < message.Length ; i+=blockSize)
            {
                StringBuilder binaryBlock = new StringBuilder();

                //внутренний цикл записывает вместе байты в двоичном виде
                for (int j = i; j < i + blockSize && j < message.Length; j++)
                {
                    binaryBlock.Append(Convert.ToString(message[j], 2).PadLeft(8, '0'));
                }

                //перевод блока из двоичного вида в десятичный
                blocks[i / blockSize] = BinaryConvertings.BinaryToBigInt(binaryBlock.ToString());
            }

            return blocks;
        }
    }
}
