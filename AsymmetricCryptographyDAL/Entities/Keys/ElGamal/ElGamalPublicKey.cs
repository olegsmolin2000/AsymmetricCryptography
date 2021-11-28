using AsymmetricCryptographyDAL.Entities.Keys.KeysVisitors;
using System.Numerics;
using System.Text;

namespace AsymmetricCryptographyDAL.Entities.Keys.ElGamal
{
    public class ElGamalPublicKey : AsymmetricKey
    {
        public BigInteger P { get; private set; }
        public BigInteger G { get; private set; }

        public BigInteger Y { get; private set; }

        private ElGamalPublicKey(string name, int binarySize)
            : base(name, "ElGamal", "Public", binarySize) { }

        public ElGamalPublicKey(string name, int binarySize, BigInteger y,BigInteger p,BigInteger g)
            : this(name, binarySize)
        {
            this.P = p;
            this.G = g;

            this.Y = y;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GetInfo());

            result.Append("Y:" + Y + "\n");

            return result.ToString();
        }

        public override void Accept(IKeyVisitor keyVisitor)
        {
            keyVisitor.VisitElGamalPublicKey(this);
        }
    }
}
