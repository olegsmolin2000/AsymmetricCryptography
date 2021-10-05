using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography.CryptographicHash
{
    abstract class CryptographicHashAlgorithm
    {
        //константные байты для предобработки данных
        protected static readonly byte OffsetFirstByte = 0x80;
        protected static readonly byte OffsetEmptyByte = 0x00;

        //константы размеров в байтах
        //размер блока
        protected const int BLOCK_SIZE = 64;
        //размер для записи битовой длины данных
        protected const int LENGTH_SIZE = 8;
        //размер маркера
        protected const int FIRST_OFFSET_BYTE_SIZE = 1;

        //метод получения длины хеша в битах
        public abstract int GetDigestBitLength();
        //метод получения хеша в виде массива байтов
        public abstract byte[] GetHash(byte[] message);

        //операция циклического битового сдвига вправо
        protected UInt32 RotateRight(UInt32 word, int rotateCount)
        {
            return (word >> rotateCount) | (word << (32 - rotateCount));
        }

        //операция циклического битового сдвига влево
        protected UInt32 RotateLeft(UInt32 word, int rotateCount)
        {
            return (word << rotateCount) | (word >> (32 - rotateCount));
        }
    }
}
