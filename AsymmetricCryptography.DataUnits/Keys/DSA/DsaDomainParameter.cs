using System.Collections.Generic;

namespace AsymmetricCryptography.DataUnits.Keys.DSA
{
    public sealed class DsaDomainParameter : AsymmetricKey
    {
        public BigInteger Q { get; init; }
        public BigInteger P { get; init; }
        public BigInteger G { get; init; }

        public List<DsaPrivateKey> PrivateKeys { get; set; }
        public List<DsaPublicKey> PublicKeys { get; set; }

        private DsaDomainParameter(int binarySize) 
            : base(AlgorithmName.DSA, KeyType.DomainParameter, binarySize) { }

        public DsaDomainParameter(int binarySize, BigInteger q, BigInteger p, BigInteger g)
            : this(binarySize)
        {
            Q = q;
            P = p;
            G = g;
        }
    }
}
