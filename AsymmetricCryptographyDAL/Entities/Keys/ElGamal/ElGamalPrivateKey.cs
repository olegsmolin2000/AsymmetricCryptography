using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.ElGamal
{
    public class ElGamalPrivateKey : AsymmetricKey
    {
        public BigInteger P { get; private set; }
        public BigInteger G { get; private set; }

        public BigInteger X { get; private set; }

        private ElGamalPrivateKey(string name, int binarySize)
            : base(name, "ElGamal", "Private", binarySize) { }

        public ElGamalPrivateKey(string name, int binarySize, BigInteger x, BigInteger p, BigInteger g)
            : this(name, binarySize)
        {
            this.P = p;
            this.G = g;

            this.X = x;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("X:" + X + "\n");

            return result.ToString();
        }
    }
}
