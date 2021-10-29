using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.ElGamal
{
    public class ElGamalPublicKey:AsymmetricKey
    {
        public BigInteger Y { get; private set; }

        public int KeyParameterId { get; set; }
        public ElGamalKeyParameter KeyParameter { get; set; }

        private ElGamalPublicKey(string name, int binarySize)
            : base(name, "ElGamal", "Public", binarySize) { }

        public ElGamalPublicKey(string name, int binarySize, BigInteger y)
            : this(name, binarySize)
        {
            this.Y = y;
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
