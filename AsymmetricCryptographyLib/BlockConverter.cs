using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography
{
    //todo:
    // zero bytes incorrect encryption
    internal static class BlockConverter
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

            List<byte> bytesList = new List<byte>(message);

            if (bytesList.Count % blockSize != 0)
            {
                blocksCount++;

                while (bytesList.Count % blockSize != 0)
                    bytesList.Insert(0, 0);
            }
               

            BigInteger[] blocks = new BigInteger[blocksCount];

            //внешний цикл даёт 1 блок на каждой итерации
            for (int i = 0; i < bytesList.Count; i += blockSize)
            {
                StringBuilder binaryBlock = new StringBuilder();

                //внутренний цикл записывает вместе байты в двоичном виде
                for (int j = i; j < i + blockSize && j < bytesList.Count; j++)
                {
                    binaryBlock.Append(Convert.ToString(bytesList[j], 2).PadLeft(8, '0'));
                }

                //перевод блока из двоичного вида в десятичный
                blocks[i / blockSize] = BinaryConverter.BinaryToBigInt(binaryBlock.ToString());
            }



            //for (int i = message.Length - 1; i >= 0; i -= blockSize)
            //{
            //    StringBuilder binaryBlock = new StringBuilder();

            //    //внутренний цикл записывает вместе байты в двоичном виде
            //    for (int j = i; j >= i - blockSize && j >= 0; j--)
            //    {
            //        binaryBlock.Append(Convert.ToString(message[j], 2).PadLeft(8, '0'));
            //    }

            //    //перевод блока из двоичного вида в десятичный
            //    blocks[i / blockSize] = BinaryConverter.BinaryToBigInt(binaryBlock.ToString());
            //}



            return blocks;
        }

        //получение массива байтов из блока
        public static byte[] BlockToBytes(BigInteger block, int bytesCount = 0)
        {
            //перевод блока в двоичный вид
            string binaryBlock = BinaryConverter.BigIntToBinary(block);

            byte[] blockBytes;

            if (bytesCount == 0)
            {
                //дописывание нулей в начало, чтобы получить точные байты из блока
                if (binaryBlock.Length % 8 != 0)
                {
                    int offsetCount = 8 - binaryBlock.Length % 8;

                    binaryBlock = binaryBlock.PadLeft(binaryBlock.Length + offsetCount, '0');
                }

                blockBytes = new byte[binaryBlock.Length / 8];
            }
            else
            {
                blockBytes = new byte[bytesCount];

                binaryBlock = binaryBlock.PadLeft(bytesCount * 8, '0');
            }

            for (int i = 0; i < binaryBlock.Length; i+=8)
            {
                string binaryByte = binaryBlock.Substring(i, 8);

                blockBytes[i/8]= Convert.ToByte(binaryByte, 2);
            }

            return blockBytes;
        }
    }
}
