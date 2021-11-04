using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.ElGamal
{
    public class ElGamalPrivateKey : AsymmetricKey
    {
        public BigInteger X { get; private set; }

        public int KeyParameterId { get; set; }
        public ElGamalKeyParameter KeyParameter { get; set; }

        private ElGamalPrivateKey(string name,int binarySize,string[] generationParameters)
            :base(name,"ElGamal","Private",binarySize, generationParameters) { }

        public ElGamalPrivateKey(string name,int binarySize,string[] generationParameters, BigInteger x)
            :this(name,binarySize, generationParameters)
        {
            this.X = x;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("\nKeyParameter:\n");
            result.Append(KeyParameter.ToString());
            result.Append("\n");

            result.Append("X:" + X + "\n");

            return result.ToString();
        }
    }
}
