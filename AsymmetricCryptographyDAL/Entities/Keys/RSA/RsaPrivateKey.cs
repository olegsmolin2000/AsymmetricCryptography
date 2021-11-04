using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.RSA
{
    public class RsaPrivateKey : AsymmetricKey
    {
        public BigInteger Modulus { get; private set; }//n
        public BigInteger PrivateExponent { get; private set; }//d

        // ctor for ef core
        private RsaPrivateKey(string name, int binarySize, string[] generationParameters)
            : base(name, "RSA", "Private", binarySize,generationParameters) { }

        public RsaPrivateKey(string name, int binarySize, string[] generationParameters, BigInteger modulus, BigInteger privateExponent)
            : this(name, binarySize, generationParameters)
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
