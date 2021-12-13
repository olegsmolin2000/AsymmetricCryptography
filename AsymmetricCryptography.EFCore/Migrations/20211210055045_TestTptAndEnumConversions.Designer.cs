﻿// <auto-generated />
using AsymmetricCryptography.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AsymmetricCryptography.EFCore.Migrations
{
    [DbContext(typeof(KeysContext))]
    [Migration("20211210055045_TestTptAndEnumConversions")]
    partial class TestTptAndEnumConversions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AsymmetricCryptography.DataUnits.Keys.AsymmetricKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AlgorithmName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BinarySize")
                        .HasColumnType("int");

                    b.Property<string>("HashAlgorithm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberGenerator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimalityVerificator")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Keys", (string)null);
                });

            modelBuilder.Entity("AsymmetricCryptography.DataUnits.Keys.ElGamal.ElGamalPrivateKey", b =>
                {
                    b.HasBaseType("AsymmetricCryptography.DataUnits.Keys.AsymmetricKey");

                    b.Property<string>("G")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("P")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("X")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("ElGamalPrivateKeys", (string)null);
                });

            modelBuilder.Entity("AsymmetricCryptography.DataUnits.Keys.ElGamal.ElGamalPublicKey", b =>
                {
                    b.HasBaseType("AsymmetricCryptography.DataUnits.Keys.AsymmetricKey");

                    b.Property<string>("G")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("P")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Y")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("ElGamalPublicKeys", (string)null);
                });

            modelBuilder.Entity("AsymmetricCryptography.DataUnits.Keys.RSA.RsaPrivateKey", b =>
                {
                    b.HasBaseType("AsymmetricCryptography.DataUnits.Keys.AsymmetricKey");

                    b.Property<string>("Modulus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrivateExponent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("RsaPrivateKeys", (string)null);
                });

            modelBuilder.Entity("AsymmetricCryptography.DataUnits.Keys.RSA.RsaPublicKey", b =>
                {
                    b.HasBaseType("AsymmetricCryptography.DataUnits.Keys.AsymmetricKey");

                    b.Property<string>("Modulus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicExponent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("RsaPublicKeys", (string)null);
                });

            modelBuilder.Entity("AsymmetricCryptography.DataUnits.Keys.ElGamal.ElGamalPrivateKey", b =>
                {
                    b.HasOne("AsymmetricCryptography.DataUnits.Keys.AsymmetricKey", null)
                        .WithOne()
                        .HasForeignKey("AsymmetricCryptography.DataUnits.Keys.ElGamal.ElGamalPrivateKey", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AsymmetricCryptography.DataUnits.Keys.ElGamal.ElGamalPublicKey", b =>
                {
                    b.HasOne("AsymmetricCryptography.DataUnits.Keys.AsymmetricKey", null)
                        .WithOne()
                        .HasForeignKey("AsymmetricCryptography.DataUnits.Keys.ElGamal.ElGamalPublicKey", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AsymmetricCryptography.DataUnits.Keys.RSA.RsaPrivateKey", b =>
                {
                    b.HasOne("AsymmetricCryptography.DataUnits.Keys.AsymmetricKey", null)
                        .WithOne()
                        .HasForeignKey("AsymmetricCryptography.DataUnits.Keys.RSA.RsaPrivateKey", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AsymmetricCryptography.DataUnits.Keys.RSA.RsaPublicKey", b =>
                {
                    b.HasOne("AsymmetricCryptography.DataUnits.Keys.AsymmetricKey", null)
                        .WithOne()
                        .HasForeignKey("AsymmetricCryptography.DataUnits.Keys.RSA.RsaPublicKey", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}