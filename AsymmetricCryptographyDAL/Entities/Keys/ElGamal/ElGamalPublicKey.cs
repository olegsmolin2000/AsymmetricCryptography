using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.ElGamal
{
    public class ElGamalPublicKey:AsymmetricKey
    {
        public BigInteger Y { get; private set; }

        public int KeyParameterId { get; set; }
        public ElGamalKeyParameter KeyParameter { get; set; }

        private ElGamalPublicKey(string name, int binarySize, string[] generationParameters)
            : base(name, "ElGamal", "Public", binarySize, generationParameters) { }

        public ElGamalPublicKey(string name, int binarySize, ElGamalKeyParameter keyParameter, string[] generationParameters, BigInteger y)
            : this(name, binarySize, generationParameters)
        {
            this.Y = y;

            this.KeyParameter = keyParameter;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("\nKeyParameter:\n");
            result.Append(KeyParameter.ToString());
            result.Append("\n");

            result.Append("Y:" + Y + "\n");

            return result.ToString();
        }
    }
}
