using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.RSA
{
    public class RsaPublicKey : AsymmetricKey
    {
        public BigInteger Modulus { get; private set; }//n
        public BigInteger PublicExponent { get; private set; }//e

        //ctor for ef core
        private RsaPublicKey(string name, int binarySize,string[] generationParameters)
            : base(name, "RSA", "Private", binarySize, generationParameters) { }

        public RsaPublicKey(string name, int binarySize,string[] generationParameters, BigInteger modulus, BigInteger publicExponent)
            : this(name, binarySize, generationParameters)
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
