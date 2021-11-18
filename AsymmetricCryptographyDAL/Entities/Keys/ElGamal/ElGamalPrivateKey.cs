using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.ElGamal
{
    public class ElGamalPrivateKey : AsymmetricKey
    {
        public BigInteger X { get; private set; }

        public virtual ElGamalKeyParameter KeyParameter { get; set; }

        private ElGamalPrivateKey(string name, int binarySize)
            : base(name, "ElGamal", "Private", binarySize) { }

        public ElGamalPrivateKey(string name, int binarySize, ElGamalKeyParameter keyParameter, BigInteger x)
            : this(name, binarySize)
        {
            this.X = x;

            this.KeyParameter = keyParameter;
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
