using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using AsymmetricCryptographyDAL.Entities.Keys.ElGamal;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;

namespace AsymmetricCryptographyDAL.Entities.Keys.KeysVisitors
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
