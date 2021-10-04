using System;
using System.Numerics;
using System.Text;
using System.Threading;

namespace AsymmetricCryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            int size = rand.Next(50);
            int blockSize = rand.Next(1,10);

            //size = 9;
            //blockSize = 8;

            Console.WriteLine("size:" + size);
            Console.WriteLine("blockSize:" + blockSize);

            Console.WriteLine("----------------------------");

            byte[] bytes = new byte[size];

            for (int i = 0; i < size; i++)
            {
                bytes[i] = Convert.ToByte(rand.Next() % 256);

                Console.WriteLine(Convert.ToString(bytes[i], 2).PadLeft(8, '0'));
            }

            BigInteger[] blocks = BlockConverter.BytesToBlocks(bytes, blockSize);

            Console.WriteLine("----------------------------");
            for (int i = 0; i < blocks.Length; i++)
            {
                Console.WriteLine(BinaryConvertings.BigIntToBinary(blocks[i]).PadLeft(blockSize * 8, '0'));
                byte[] bytesblocks = BlockConverter.BlockToBytes(blocks[i], blockSize);

                for (int j = 0; j < bytesblocks.Length; j++)
                {
                    Console.WriteLine("\t" + Convert.ToString(bytesblocks[j], 2).PadLeft(8, '0'));
                }
            }

        }
    }
}
