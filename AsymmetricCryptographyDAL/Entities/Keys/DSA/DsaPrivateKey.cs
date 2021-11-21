﻿using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.DSA
{
    public class DsaPrivateKey : AsymmetricKey
    {
        public BigInteger X { get; private set; }

        public virtual DsaDomainParameter DomainParameter { get; set; }

        private DsaPrivateKey(string name, int binarySize)
            : base(name, "DSA", "Private", binarySize) { }

        public DsaPrivateKey(string name, int binarySize, DsaDomainParameter domainParameter, BigInteger x)
            : this(name, binarySize)
        {
            this.X = x;

            this.DomainParameter = domainParameter;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("\nDomainParameter:\n");
            result.Append(DomainParameter.ToString());
            result.Append("\n");

            result.Append("X:" + X + "\n");

            return result.ToString();
        }
    }
}