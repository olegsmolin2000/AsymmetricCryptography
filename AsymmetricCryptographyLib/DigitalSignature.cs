namespace AsymmetricCryptography
{
    public abstract class DigitalSignature
    {
        public virtual string Type { get; }

        public abstract override string ToString();
    }
}
