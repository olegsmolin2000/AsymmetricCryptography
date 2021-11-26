using System;
using System.Collections.Generic;
using System.Text;
using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptography.PrimalityVerificators;
using AsymmetricCryptography.RandomNumberGenerators;

namespace AsymmetricCryptography
{
    public class GeneratingParameters
    {
        public NumberGenerator numberGenerator { get; set; }
        public PrimalityVerificator primalityVerificator { get; set; }
        public CryptographicHashAlgorithm hashAlgorithm { get; set; }

        public GeneratingParameters(NumberGenerator numberGenerator,PrimalityVerificator primalityVerificator,CryptographicHashAlgorithm hashAlgorithm)
        {
            this.numberGenerator = numberGenerator;
            this.primalityVerificator = primalityVerificator;
            this.hashAlgorithm = hashAlgorithm;
        }

        public string[] GetParameters()
        {
            string[] parameters = new string[3];

            parameters[0] = numberGenerator.ToString();
            parameters[1] = primalityVerificator.ToString();
            parameters[2] = hashAlgorithm.ToString();

            return parameters;
        }

        public static GeneratingParameters GetParametersByInfo(string[] info)
        {
            PrimalityVerificator verificator;
            NumberGenerator generator;
            CryptographicHashAlgorithm cryptographicHash;

            switch (info[1])
            {
                case "Miller-Rabin":
                    {
                        verificator = new MillerRabinPrimalityVerificator();
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }

            

            switch (info[0])
            {
                case "Lagged Fibonacci":
                    {
                        generator = new FibonacciNumberGenerator(verificator);
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }

            switch (info[2])
            {
                case "SHA-256":
                    {
                        cryptographicHash = new SHA_256();
                        break;
                    }
                case "MD-5":
                    {
                        cryptographicHash = new MD_5();
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }

            return new GeneratingParameters(generator, verificator, cryptographicHash);
        }
    }
}
