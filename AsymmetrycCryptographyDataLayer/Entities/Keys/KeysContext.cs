using AsymmetrycCryptographyDataLayer.Entities;
using AsymmetrycCryptographyDataLayer.Entities.Keys;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace AsymmetrycCryptographyDataLayer.Entities.Keys
{
    public class KeysContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CryptographyDB;");
        }

        public KeysContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<RsaPrivateKey>().ToTable("RsaPrivateKeys");
            //modelBuilder.Entity<RsaPublicKey>().ToTable("RsaPublicKey");

            //// для каждой модели все её свойства типа BigInteger конвертируются в string
            //foreach (var entity in modelBuilder.Model.GetEntityTypes())
            //{
            //    foreach (var property in entity.ClrType.GetProperties())
            //    {
            //        if (property.PropertyType == typeof(BigInteger))
            //            modelBuilder.Entity(entity.Name).Property(property.Name).HasConversion<string>();
            //    }
            //}
        }

        public DbSet<AsymmetricKey> Keys { get; set; }
        //public DbSet<RsaPrivateKey> RsaPrivateKeys { get; set; }
        //public DbSet<RsaPublicKey> RsaPublicKeys { get; set; }
    }
}
