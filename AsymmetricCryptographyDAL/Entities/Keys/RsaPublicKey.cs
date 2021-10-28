using System.Numerics;

namespace AsymmetricCryptographyDAL.Entities.Keys
{
    public class RsaPublicKey : AsymmetricKey
    {
        public BigInteger Modulus { get; private set; }//n
        public BigInteger PublicExponent { get; private set; }//e

        //ctor for ef core
        private RsaPublicKey(string name, int binarySize)
            : base(name, "RSA", "Private", binarySize) { }

        public RsaPublicKey(string name, int binarySize, BigInteger modulus, BigInteger publicExponent)
            : this(name, binarySize)
        {
            this.Modulus = modulus;
            this.PublicExponent = publicExponent;
        }
    }
}
