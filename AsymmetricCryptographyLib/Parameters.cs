﻿using System;
using System.Collections.Generic;
using System.Text;
using AsymmetricCryptography.CryptographicHash;

namespace AsymmetricCryptography
{
    public class Parameters
    {
        public NumberGenerator numberGenerator { get; set; }
        public PrimalityVerificator primalityVerificator { get; set; }
        public CryptographicHashAlgorithm hashAlgorithm { get; set; }

        public Parameters(NumberGenerator numberGenerator,PrimalityVerificator primalityVerificator,CryptographicHashAlgorithm hashAlgorithm)
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
    }
}
