namespace AsymmetricCryptography.Core.PrimalityVerification
{
    public abstract class PrimalityVerificator
    {
        public static PrimalityVerificator GetPrimalityVerificator(PrimalityVerifiator primalityVerifiator)
        {
            return null;
        }
    }

    public enum PrimalityVerifiator
    {
        MillerRabin
    }
}
