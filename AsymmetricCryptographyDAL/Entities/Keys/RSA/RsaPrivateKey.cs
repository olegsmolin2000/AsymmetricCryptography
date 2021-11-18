using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.RSA
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

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("Modulus (n):" + Modulus + "\n");
            result.Append("PrivateExponent (d):" + PrivateExponent + "\n");

            return result.ToString();
        }
    }
}
