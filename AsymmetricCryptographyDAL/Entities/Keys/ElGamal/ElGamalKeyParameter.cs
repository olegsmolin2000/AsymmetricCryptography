using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.ElGamal
{
    public class ElGamalKeyParameter:AsymmetricKey
    {
        public BigInteger P { get; private set; }
        public BigInteger G { get; private set; }

        public List<ElGamalPrivateKey> ElGamalPrivateKeys { get; set; }
        public List<ElGamalPublicKey> ElGamalPublicKeys { get; set; }

        private ElGamalKeyParameter(string name,int binarySize,string[] generationParameters)
            :base(name,"ElGamal","Parameter",binarySize, generationParameters) { }

        public ElGamalKeyParameter(string name,int binarySize,string[] generationParameters, BigInteger p,BigInteger g)
            :this(name,binarySize, generationParameters)
        {
            this.P = p;
            this.G = g;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("P:" + P + "\n");
            result.Append("G:" + G + "\n");

            return result.ToString();
        }
    }
}
