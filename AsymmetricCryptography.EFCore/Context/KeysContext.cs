using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Numerics;

namespace AsymmetricCryptography.EFCore.Context
{
    public class KeysContext:DbContext
    {
        public DbSet<AsymmetricKey> Keys { get; set; }

        public DbSet<DsaDomainParameter> DsaDomainParameters { get; set; }
        public DbSet<DsaPrivateKey> DsaPrivateKeys { get; set; }
        public DbSet<DsaPublicKey> DsaPublicKeys { get; set; }

        public DbSet<ElGamalPrivateKey> ElGamalPrivateKeys { get; set; }
        public DbSet<ElGamalPublicKey> ElGamalPublicKeys { get; set; }

        public DbSet<RsaPrivateKey> RsaPrivateKeys { get; set; }
        public DbSet<RsaPublicKey> RsaPublicKeys { get; set; }

        public KeysContext()
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

            #region Conversions
            modelBuilder
                .Entity<AsymmetricKey>()
                .Property(key => key.AlgorithmName)
                .HasConversion<string>();

            modelBuilder
                .Entity<AsymmetricKey>()
                .Property(key => key.KeyType)
                .HasConversion<string>();

            var BigIntToStringConverter = new ValueConverter<BigInteger, string>
               (BigInt => BigInt.ToString(),
               str => BigInteger.Parse(str));

            // для каждой модели все её свойства типа BigInteger
            // при записи в БД конвертируются в string 
            // при чтении конвертируются обратно в BigInteger
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
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CryptographyDB;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
