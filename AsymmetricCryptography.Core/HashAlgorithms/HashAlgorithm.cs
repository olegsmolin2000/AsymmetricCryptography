namespace AsymmetricCryptography.Core.HashAlgorithms
{
    /// <summary>
    /// Represents the base class from which all implementations of hash algorithms must derived
    /// </summary>
    public abstract class HashAlgorithm
    {
        /// <summary>
        /// Computes the hash of data using the cryptographic hash algorithm.
        /// </summary>
        /// <param name="data">Message to getting hash</param>
        /// <returns>Byte array of the digest</returns>
        public abstract byte[] GetHash(byte[] data);

        /// <summary>
        /// Bits count of the digest
        /// </summary>
        public abstract int DigestBitSize { get; }

        /// <summary>
        /// Pad message with empty bytes to make correct blocks dividing
        /// </summary>
        /// <param name="data">Byte array of the message</param>
        /// <param name="blockBytesCount">Bytes count in each block</param>
        /// <param name="markBytesCount">Bytes count of the marker</param>
        /// <param name="dataLengthBytesCount">Bytes count for writing message bits count</param>
        /// <returns>Preprocessed message with empty bytes at the end</returns>
        /// <exception cref="ArgumentException"></exception>
        protected byte[] Preprocess(byte[] data, int blockBytesCount,int markBytesCount, int dataLengthBytesCount)
        {
            // количество байт в сообщении
            int dataBytesCount = data.Length;

            if (blockBytesCount < markBytesCount + dataLengthBytesCount)
                throw new ArgumentException();

            // количество свободных байт, которыми можно дополнить сообщение
            // до размера, кратному blockBytesCount
            int freeBytesInLastBlock = blockBytesCount - (dataBytesCount % blockBytesCount);

            // количество байт, которыми будет дополняться исходное сообщение,
            // чтобы получить массив байтов, пригодный для разделения на блоки
            // с учётом вставки маркера и длины сообщения
            int newBytesCount = 0;

            // если нет свободных байтов то сообщение будет дополняться
            // количеством байтов, равным размеру блока
            if (freeBytesInLastBlock == 0)
                newBytesCount = blockBytesCount;

            // если в свободном места не хватает количества байт для записи маркера и
            // длины сообщения, то сообщение дополнится до конца блока, плюс
            // будет добавлен ещё один блок
            if (freeBytesInLastBlock < markBytesCount + dataLengthBytesCount)
            {
                newBytesCount = freeBytesInLastBlock + blockBytesCount;
            }

            // если дополнить сообщение до блока и в нём хватит места для маркера и длины сообщения,
            // то просто дополняется свободными байтами
            if (freeBytesInLastBlock >= markBytesCount + dataLengthBytesCount)
                newBytesCount = freeBytesInLastBlock;

            int preprocessDataBytesCount = dataBytesCount + newBytesCount;

            byte[] preprocessedData = new byte[preprocessDataBytesCount];

            // заполнение предобработанного массива байтов исходными данными
            Array.Copy(data, preprocessedData, dataBytesCount);

            return preprocessedData;
        }

        /// <summary>
        /// Right logical shift
        /// </summary>
        /// <param name="word"></param>
        /// <param name="rotateCount"></param>
        /// <returns></returns>
        protected UInt32 RotateRight(UInt32 word, int rotateCount)
        {
            return (word >> rotateCount) | (word << (32 - rotateCount));
        }

        /// <summary>
        /// Left logical shift
        /// </summary>
        /// <param name="word"></param>
        /// <param name="rotateCount"></param>
        /// <returns></returns>
        protected UInt32 RotateLeft(UInt32 word, int rotateCount)
        {
            return (word << rotateCount) | (word >> (32 - rotateCount));
        }
    }
}
