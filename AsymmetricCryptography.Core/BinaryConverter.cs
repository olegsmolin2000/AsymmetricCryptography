using System.Text;

namespace AsymmetricCryptography.Core
{
    public static class BinaryConverter
    {
        /// <summary>
        /// Convert BigInteger value to the binary representation of a number
        /// </summary>
        /// <param name="number">Value to convert</param>
        /// <returns>Binary representation in string</returns>
        public static string ToBinaryString(this BigInteger number)
        {
            StringBuilder binary = new StringBuilder();

            while(number != 0)
            {
                binary.Append(number % 2);

                number >>= 1;
            }

            return new string(binary.ToString().Reverse().ToArray());
        }

        /// <summary>
        /// Convert string binary representation of a number to the BigInteger instance
        /// </summary>
        /// <param name="binary">Binary representation in string</param>
        /// <returns>BigInteger instance</returns>
        public static BigInteger FromBinaryString(this string binary)
        {
            BigInteger number = 0;

            for (int i = 0; i < binary.Length; i++)
            {
                number <<= 1;
                number += (binary[i] == '1') ? 1 : 0;
            }

            return number;
        }
    }
}
