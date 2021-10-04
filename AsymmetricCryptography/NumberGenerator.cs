using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography
{
    static class NumberGenerator
    {
        private static Random Rand;

        //список запаздываний для метода Фибоначчи с запазываниями
        private static readonly List<(int, int)> Laggs = new List<(int, int)>()
        {
            (24, 55), (38, 89), (37, 100), (30, 127),
            (83, 258), (107, 378), (273, 607), (1029, 2281),
            (576, 3217), (4187, 9689), (7083, 19937), (9739, 23209)
        };

        static NumberGenerator()
        {
            Rand = new Random();
        }

        //генерация случайного числа по количеству бит методом Фибоначчи с запаздываниями
        //в полученом случайном числе ровно столько бит, сколько задано параметром
        public static BigInteger GenerateNumber(int binaryLength)
        {
            //выбор запаздываний по битовой длине генерируемового числа
            int laggsIndex = -1;

            if (binaryLength > Laggs[Laggs.Count - 1].Item2)
                laggsIndex = Laggs.Count - 1;
            else
            {
                for (int i = 0; i < Laggs.Count && laggsIndex == -1; i++) 
                {
                    if (Laggs[i].Item2 > binaryLength)
                        laggsIndex = Math.Max(0, i - 1);
                }
            }
            
            int a = Laggs[laggsIndex].Item2;
            int b = Laggs[laggsIndex].Item1;

            StringBuilder number = new StringBuilder();

            if (binaryLength > Math.Max(a, b))
            {
                for (int i = 0; i < Math.Max(a, b); i++)
                {
                    int randBit = Rand.Next(2);

                    number.Append(randBit);
                }

                for (int i = Math.Max(a, b); number.Length < binaryLength; i++)
                {
                    int left = Convert.ToInt32(number[i - a]);
                    int right = Convert.ToInt32(number[i - b]);

                    int summ = ((left + right) % 2);

                    number.Append(summ);
                }
            }
            else
            {
                //если битовый размер числа меньше запаздываний, то разряды заполняются случайными битами
                while (number.Length < binaryLength)
                {
                    int randBit = Rand.Next(2);

                    number.Append(randBit);
                }
            }

            //если первый бит получился нулевым, то он меняется на единицу
            //для того, чтобы полученное число имело столько бит, сколько задано пользователем
            if (number[0] == '0')
                number[0] = '1';

            return BinaryConverter.BinaryToBigInt(number.ToString());
        }

        //генерация числа по диапазону
        public static BigInteger GenerateNumber(BigInteger min,BigInteger max)
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
                int newNumberBitLength = Rand.Next(minBitLength, maxBitLength + 1);

                number = GenerateNumber(newNumberBitLength);
            } while (number < min || number > max);

            return number;
        }

        //генерация простых чисел
        //генерируются случайные числа до тех пор, пока не выпадет число, прошедшее проверку на простоту
        public static BigInteger GeneratePrimeNumber(int binarylength)
        {
            BigInteger number = 0;

            while (!PrimalityVerifications.IsPrimal(number,100))
                number = GenerateNumber(binarylength);

            return number;
        }

        public static BigInteger GeneratePrimeNumber(BigInteger min,BigInteger max)
        {
            BigInteger number = 0;

            while (!PrimalityVerifications.IsPrimal(number, 100))
                number = GenerateNumber(min, max);

            return number;
        }
    }
}
