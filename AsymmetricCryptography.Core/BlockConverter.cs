using System.Text;

namespace AsymmetricCryptography.Core
{
    /// <summary>
    /// Provides static methods for converting bytes to bigint and back
    /// </summary>
    internal static class BlockConverter
    {
        private const int BYTE_SIZE = 8;

        /// <summary>
        /// Compute bytes count can be process by modulus without losses
        /// </summary>
        /// <param name="modulus">Modulus to find bytes count</param>
        /// <returns>Bytes count</returns>
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

        /// <summary>
        /// Split byte array into big int values
        /// </summary>
        /// <param name="message">Data to split</param>
        /// <param name="blockSize">Bytes count to make block</param>
        /// <returns>Block array</returns>
        public static BigInteger[] BytesToBlocks(byte[] message, int blockSize)
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
                    binaryBlock.Append(Convert.ToString(bytesList[j], 2).PadLeft(BYTE_SIZE, '0'));
                }

                //перевод блока из двоичного вида в десятичный
                blocks[i / blockSize] = binaryBlock.ToString().FromBinaryString();
            }

            return blocks;
        }

        /// <summary>
        /// Split the block into byte array
        /// </summary>
        /// <param name="block">Big int block value</param>
        /// <param name="bytesCount">Count of bytes in this block</param>
        /// <returns>Block converted to byte array</returns>
        public static byte[] BlockToBytes(BigInteger block, int bytesCount = 0)
        {
            //перевод блока в двоичный вид
            string binaryBlock = block.ToBinaryString();

            byte[] blockBytes;

            if (bytesCount == 0)
            {
                //дописывание нулей в начало, чтобы получить точные байты из блока
                if (binaryBlock.Length % BYTE_SIZE != 0)
                {
                    int offsetCount = BYTE_SIZE - binaryBlock.Length % BYTE_SIZE;

                    binaryBlock = binaryBlock.PadLeft(binaryBlock.Length + offsetCount, '0');
                }

                blockBytes = new byte[binaryBlock.Length / BYTE_SIZE];
            }
            else
            {
                blockBytes = new byte[bytesCount];

                binaryBlock = binaryBlock.PadLeft(bytesCount * BYTE_SIZE, '0');
            }

            for (int i = 0; i < binaryBlock.Length; i += BYTE_SIZE)
            {
                string binaryByte = binaryBlock.Substring(i, BYTE_SIZE);

                blockBytes[i / BYTE_SIZE] = Convert.ToByte(binaryByte, 2);
            }

            return blockBytes;
        }
    }
}
