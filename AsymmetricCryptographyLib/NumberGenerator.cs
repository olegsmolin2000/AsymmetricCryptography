using System;
using System.Numerics;

namespace AsymmetricCryptography
{
    public abstract class NumberGenerator
    {
        protected static Random rand = new Random();

        protected PrimalityVerificator primalityVerificator;

        protected NumberGenerator(PrimalityVerificator primalityVerificator)
        {
            this.primalityVerificator = primalityVerificator;
        }

        public abstract BigInteger GenerateNumber(int binarySize);
        //генерация числа по диапазону
        public BigInteger GenerateNumber(BigInteger min, BigInteger max)
        {
            if (min > max)
                throw new ArgumentException("Min > max");

            int minBitLength = BinaryConverter.GetBinaryLength(min);
            int maxBitLength = BinaryConverter.GetBinaryLength(max);

            BigInteger number;


            //выбирается случайная битовая длина между минимальной и максимальной границами
            //и по ней генерируется число в нужных пределах
            do
            {
                int newNumberBitLength = rand.Next(minBitLength, maxBitLength + 1);

                number = GenerateNumber(newNumberBitLength);
            } while (number < min || number > max);

            return number;
        }

        //генерация простых чисел
        //генерируются случайные числа до тех пор, пока не выпадет число, прошедшее проверку на простоту
        public BigInteger GeneratePrimeNumber(int binarySize)
        {
            BigInteger number = 0;

            while (!primalityVerificator.IsPrimal(number, 100))
                number = GenerateNumber(binarySize);

            return number;
        }

        public BigInteger GeneratePrimeNumber(BigInteger min, BigInteger max)
        {
            BigInteger number = 0;

            while (!primalityVerificator.IsPrimal(number, 100))
                number = GenerateNumber(min, max);

            return number;
        }

        public abstract override string ToString();
    }
}
