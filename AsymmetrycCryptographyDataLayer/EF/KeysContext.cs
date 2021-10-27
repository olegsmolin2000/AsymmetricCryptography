using AsymmetrycCryptographyDataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace AsymmetrycCryptographyDataLayer.EF
{
    class KeysContext:DbContext
    {
        public KeysContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // для каждой модели все её свойства типа BigInteger конвертируются в string
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach(var property in entity.ClrType.GetProperties())
                {
                    if (property.PropertyType == typeof(BigInteger))
                        modelBuilder.Entity(entity.Name).Property(property.Name).HasConversion<string>();
                }
            }
        }

        public DbSet<AsymmetricKey> Keys { get; set; }
        public DbSet<RsaPrivateKey> RsaPrivateKeys { get; set; }
        public DbSet<RsaPublicKey> RsaPublicKeys { get; set; }
    }
}
