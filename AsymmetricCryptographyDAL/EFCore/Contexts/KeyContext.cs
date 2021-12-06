using AsymmetricCryptographyDAL.Entities.Keys;
using AsymmetricCryptographyDAL.Entities.Keys.DSA;
using AsymmetricCryptographyDAL.Entities.Keys.ElGamal;
using AsymmetricCryptographyDAL.Entities.Keys.RSA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Numerics;

namespace AsymmetricCryptographyDAL.EFCore.Contexts
{
    public class KeyContext : DbContext
    {
        public DbSet<AsymmetricKey> Keys { get; set; }

        public DbSet<RsaPrivateKey> RsaPrivateKeys { get; set; }
        public DbSet<RsaPublicKey> RsaPublicKeys { get; set; }

        public DbSet<DsaDomainParameter> DsaDomainParameters { get; set; }
        public DbSet<DsaPrivateKey> DsaPrivateKeys { get; set; }
        public DbSet<DsaPublicKey> DsaPublicKeys { get; set; }

        public DbSet<ElGamalPrivateKey> ElGamalPrivateKeys { get; set; }
        public DbSet<ElGamalPublicKey> ElGamalPublicKeys { get; set; }

        public KeyContext()
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Определение таблиц для классов

            modelBuilder
                .Entity<AsymmetricKey>()
                .ToTable("Keys");

            modelBuilder
                .Entity<RsaPrivateKey>()
                .ToTable("RsaPrivateKeys");

            modelBuilder
                .Entity<RsaPrivateKey>()
                .HasBaseType<AsymmetricKey>();

            modelBuilder
                .Entity<RsaPublicKey>()
                .ToTable("RsaPublicKeys");

            modelBuilder
                .Entity<DsaDomainParameter>()
                .ToTable("DsaDomainParameters");

            modelBuilder
                .Entity<DsaPrivateKey>()
                .ToTable("DsaPrivateKeys");

            modelBuilder
                .Entity<DsaPublicKey>()
                .ToTable("DsaPublicKeys");

            modelBuilder
                .Entity<ElGamalPrivateKey>()
                .ToTable("ElGamalPrivateKeys");

            modelBuilder
                .Entity<ElGamalPublicKey>()
                .ToTable("ElGamalPublicKeys");

            #endregion

            var BigIntToStringConverter = new ValueConverter<BigInteger, string>
                (BigInt => BigInt.ToString(),
                str => BigInteger.Parse(str));

            //для каждой модели все её свойства типа BigInteger конвертируются в string
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.ClrType.GetProperties())
                {
                    if (property.PropertyType == typeof(BigInteger))
                        modelBuilder
                            .Entity(entity.Name)
                            .Property(property.Name)
                            .HasConversion(BigIntToStringConverter);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=KeysDB;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
