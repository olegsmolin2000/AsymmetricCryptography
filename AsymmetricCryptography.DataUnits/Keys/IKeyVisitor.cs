using AsymmetricCryptography.DataUnits.Keys.DSA;
using AsymmetricCryptography.DataUnits.Keys.ElGamal;
using AsymmetricCryptography.DataUnits.Keys.RSA;

namespace AsymmetricCryptography.DataUnits.Keys
{
    public interface IKeyVisitor
    {
        public void VisitAsymmetricKey(AsymmetricKey asymmetricKey);

        public void VisitRsaPrivateKey(RsaPrivateKey rsaPrivateKey);
        public void VisitRsaPublicKey(RsaPublicKey rsaPublicKey);

        public void VisitDsaPrivateKey(DsaPrivateKey dsaPrivateKey);
        public void VisitDsaPublicKey(DsaPublicKey dsaPublicKey);
        public void VisitDsaDomainParameters(DsaDomainParameter dsaDomainParameter);

        public void VisitElGamalPrivateKey(ElGamalPrivateKey elGamalPrivateKey);
        public void VisitElGamalPublicKey(ElGamalPublicKey elGamalPublicKey);
    }
}
