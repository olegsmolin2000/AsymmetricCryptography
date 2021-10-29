using System.Numerics;
using System.Text;

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

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("Modulus (n):" + Modulus + "\n");
            result.Append("PublicExponent (e):" + PublicExponent + "\n");

            return result.ToString();
        }
    }
}
