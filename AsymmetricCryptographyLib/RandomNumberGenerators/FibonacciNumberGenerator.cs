using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography.RandomNumberGenerators
{
    public sealed class FibonacciNumberGenerator : NumberGenerator
    {
        //список запаздываний для метода Фибоначчи с запазываниями
        private static readonly List<(int, int)> Laggs = new List<(int, int)>()
        {
            (24, 55), (38, 89), (37, 100), (30, 127),
            (83, 258), (107, 378), (273, 607), (1029, 2281),
            (576, 3217), (4187, 9689), (7083, 19937), (9739, 23209)
        };

        public FibonacciNumberGenerator(PrimalityVerificator primalityVerificator) 
            : base(primalityVerificator) { }

        //генерация случайного числа по количеству бит методом Фибоначчи с запаздываниями
        //в полученом случайном числе ровно столько бит, сколько задано параметром
        public override BigInteger GenerateNumber(int binarySize)
        {
            //выбор запаздываний по битовой длине генерируемового числа
            int laggsIndex = -1;

            if (binarySize > Laggs[Laggs.Count - 1].Item2)
                laggsIndex = Laggs.Count - 1;
            else
            {
                for (int i = 0; i < Laggs.Count && laggsIndex == -1; i++) 
                {
                    if (Laggs[i].Item2 > binarySize)
                        laggsIndex = Math.Max(0, i - 1);
                }
            }
            
            int a = Laggs[laggsIndex].Item2;
            int b = Laggs[laggsIndex].Item1;

            StringBuilder number = new StringBuilder();

            if (binarySize > Math.Max(a, b))
            {
                for (int i = 0; i < Math.Max(a, b); i++)
                {
                    int randBit = rand.Next(2);

                    number.Append(randBit);
                }

                for (int i = Math.Max(a, b); number.Length < binarySize; i++)
                {
                    int left = Convert.ToInt32(number[i - a]);
                    int right = Convert.ToInt32(number[i - b]);

                    int summ = ((left + right) % 2);

                    number.Append(summ);
                }
            }
            else
            {
                //если битовый размер числа меньше запаздываний, то все разряды заполняются случайными битами
                while (number.Length < binarySize)
                {
                    int randBit = rand.Next(2);

                    number.Append(randBit);
                }
            }

            //если первый бит получился нулевым, то он меняется на единицу
            //для того, чтобы полученное число имело столько бит, сколько задано пользователем
            if (number[0] == '0')
                number[0] = '1';

            return BinaryConverter.BinaryToBigInt(number.ToString());
        }

        public override string ToString()
        {
            return "Lagged Fibonacci";
        }
    }
}
