using AsymmetricCryptography.Core.PrimalityVerificators;
using AsymmetricCryptography.DataUnits;
using System.Text;

namespace AsymmetricCryptography.Core.NumberGenerators
{
    /// <summary>
    /// Lagged Fibonacci pseudorandom number generator
    /// </summary>
    public sealed class FibonacciNumberGenerator : NumberGenerator
    {
        /// <summary>
        /// List of most popular lagg pairs (a,b) or (j,k)
        /// </summary>
        private static readonly List<(int, int)> Laggs = new List<(int, int)>()
        {
            (24, 55), (38, 89), (37, 100), (30, 127),
            (83, 258), (107, 378), (273, 607), (1029, 2281),
            (576, 3217), (4187, 9689), (7083, 19937), (9739, 23209)
        };

        /// <summary>
        /// Initializes a new instance of the Lagged Fibonacci Number Generator, using the Primality Verificator
        /// </summary>
        /// <param name="primalityVerificator">Primality verificator used in prime numbers generating</param>
        public FibonacciNumberGenerator(PrimalityVerificator primalityVerificator = null!)
            :base(RandomNumberGenerator.Fibonacci, primalityVerificator) { }

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

            number.Append('1');

            if (binarySize > Math.Max(a, b))
            {
                for (int i = 1; i < Math.Max(a, b); i++)
                {
                    int randBit = Rand.Next(2);

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
                    int randBit = Rand.Next(2);

                    number.Append(randBit);
                }
            }

            return number.ToString().FromBinaryString();
        }
    }
}
