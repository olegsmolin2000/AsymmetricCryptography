using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptography
{
    internal static class BinaryConverter
    {
        public static BigInteger BinaryToBigInt(string binaryString)
        {
            BigInteger currentExponent = 1, number = 0;

            for (int i = binaryString.Length - 1; i >= 0; i--)
            {
                if (binaryString[i] == '1')
                    number += currentExponent;

                currentExponent *= 2;
            }

            return number;
        }

        public static string BigIntToBinary(BigInteger number)
        {
            StringBuilder binaryNum = new StringBuilder();

            while (number != 0)
            {
                binaryNum.Append((number % 2).ToString());
                number /= 2;
            }

            return new string(
                binaryNum.ToString()
                .Reverse()
                .ToArray()
                );
        }

        public static int GetBinaryLength(BigInteger number)
        {
            int bitsCount = 0;

            while (number != 0)
            {
                number /= 2;
                bitsCount++;
            }

            return bitsCount;
        }
    }
}
