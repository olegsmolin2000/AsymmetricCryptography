using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography
{
    public abstract class AsymmetricKey
    {
        public abstract string AlgorithmName { get; }
        public abstract string Permittion { get; }

        public abstract void PrintConsole();

        public abstract override string ToString();
        public string GetInfo()
        {
            StringBuilder result = new StringBuilder();

            result.Append("Algorithm Name:" + AlgorithmName + "\n");
            result.Append("Permittion:" + Permittion + "\n");

            return result.ToString();
        }
    }
}
