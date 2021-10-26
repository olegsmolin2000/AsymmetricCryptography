
//using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace AsymmetricCryptographyWPF.Model
{
    
    class CryptographyContext:DbContext
    {
        public CryptographyContext()
            : base("CryptographyDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CryptographyContext>());
        }

        public DbSet<Key> Keys { get; set; }
        public DbSet<RsaPrivateKey> RsaPrivateKeys { get; set; }
        public DbSet<RsaPublicKey> RsaPublicKeys{ get; set; }
    }
}
