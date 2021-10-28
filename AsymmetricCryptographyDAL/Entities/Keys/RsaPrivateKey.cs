using System.Numerics;

namespace AsymmetricCryptographyDAL.Entities.Keys
{
    public class RsaPrivateKey : AsymmetricKey
    {
        public BigInteger Modulus { get; private set; }//n
        public BigInteger PrivateExponent { get; private set; }//d

        // ctor for ef core
        private RsaPrivateKey(string name, int binarySize)
            : base(name, "RSA", "Private", binarySize) { }

        public RsaPrivateKey(string name, int binarySize, BigInteger modulus, BigInteger privateExponent)
            : this(name, binarySize)
        {
            this.Modulus = modulus;
            this.PrivateExponent = privateExponent;
        }
    }
}
