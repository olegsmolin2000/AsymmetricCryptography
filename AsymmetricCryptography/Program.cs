﻿using System;
using System.Numerics;
using System.Text;
using System.Threading;
using AsymmetricCryptography.RSA;
using AsymmetricCryptography.CryptographicHash;
using AsymmetricCryptography.DigitalSignatureAlgorithm;
using AsymmetricCryptography.ElGamal;

namespace AsymmetricCryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            int s = 0;
            int count = 1000;

            for (int i = 0; i < count; i++)
            {
                PrimalityVerificator primalityVerificator = new MillerRabinPrimalityVerificator();
                NumberGenerator numberGenerator = new FibonacciNumberGenerator(primalityVerificator);
                primalityVerificator.SetNumberGenerator(numberGenerator);


                if (i % 10 == 0)
                Console.WriteLine(i);

                AsymmetricKey rsaPrivateKey;
                AsymmetricKey rsaPublicKey;
                AsymmetricKey dsaPrivateKey;
                AsymmetricKey dsaPublicKey;
                AsymmetricKey elGamalPrivateKey;
                AsymmetricKey elGamalPublicKey;

                KeysGenerator rsaGenerator = new RsaKeysGenerator(numberGenerator, primalityVerificator);
                KeysGenerator dsaGenerator = new DsaKeysGenerator(numberGenerator, primalityVerificator);
                KeysGenerator elGamalGenerator = new ElGamalKeysGenerator(numberGenerator, primalityVerificator);

                rsaGenerator.GenerateKeyPair(64, out rsaPrivateKey, out rsaPublicKey);
                ((DsaKeysGenerator)dsaGenerator).DsaKeysGeneration(48, 32, out dsaPrivateKey, out dsaPublicKey);
                elGamalGenerator.GenerateKeyPair(24, out elGamalPrivateKey, out elGamalPublicKey);

                StringBuilder message = new StringBuilder();



                int mesLength = rand.Next() % 50;

                for (int j = 0; j < mesLength; j++)
                {
                    message.Append(Convert.ToChar(Convert.ToByte(rand.Next() % 255 + 1)));
                }

                byte[] data = Encoding.UTF8.GetBytes(message.ToString());



                RsaAlgorithm rsaAlg = new RsaAlgorithm(rsaPrivateKey, rsaPublicKey, numberGenerator, primalityVerificator);
                DSA dsaAlg = new DSA(dsaPrivateKey, dsaPublicKey, numberGenerator, primalityVerificator);
                ElGamalAlgorithm elGamalAlg = new ElGamalAlgorithm(elGamalPrivateKey, elGamalPublicKey, numberGenerator, primalityVerificator);

                DigitalSignature rsaSign = rsaAlg.CreateSignature(data, new SHA_256());
                DigitalSignature dsaSign = dsaAlg.CreateSignature(data, new SHA_256());
                DigitalSignature elGamalsign = elGamalAlg.CreateSignature(data, new SHA_256());

                bool rsaVer = rsaAlg.VerifyDigitalSignature(rsaSign, data, new SHA_256());
                bool dsaVer = dsaAlg.VerifyDigitalSignature(dsaSign, data, new SHA_256());
                bool elgamalVer = elGamalAlg.VerifyDigitalSignature(elGamalsign, data, new SHA_256());


                var rsaEncryption = rsaAlg.Encrypt(data);
                var elGamalEncryption = elGamalAlg.Encrypt(data);

                var rsaDecryption = rsaAlg.Decrypt(rsaEncryption);
                var elGamalDecryption = elGamalAlg.Decrypt(elGamalEncryption);

                if (!rsaVer)
                    Console.WriteLine("rsa sign error");
                if (!dsaVer)
                    Console.WriteLine("dsa sign error");
                if (!elgamalVer)
                    Console.WriteLine("el gamal sign error");
                
                if (Encoding.UTF8.GetString(rsaDecryption) != message.ToString())
                    Console.WriteLine("rsa crypt error");
                if (Encoding.UTF8.GetString(elGamalDecryption) != message.ToString())
                    Console.WriteLine("el gamal crypt error");
                

            }


            Console.WriteLine("s:" + s);
            Console.WriteLine("count:" + count);


            //int keySize = 0;

            //Console.Write("input key binary size:");
            //keySize = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine();
            //RsaAlgorithm rsa = new RsaAlgorithm(keySize);
            //rsa.PrintKeys();
            //string message = "";
            //Console.WriteLine();
            //Console.Write("input messsage:");
            //message = Console.ReadLine();

            //byte[] mesBytes = Encoding.UTF8.GetBytes(message);

            //byte[] encrypt = rsa.Encrypt(mesBytes);

            //Console.WriteLine();

            //Console.WriteLine("encryption:"+Encoding.UTF8.GetString(encrypt));
            //Console.WriteLine();
            //byte[] decrypt = rsa.Decrypt(encrypt);

            //Console.WriteLine("decryption:" + Encoding.UTF8.GetString(decrypt));
            //Console.WriteLine();
            //CryptographicHashAlgorithm hashAlgorithm = new SHA_256();

            //DigitalSignature sign = rsa.CreateSignature(mesBytes, hashAlgorithm);

            //sign.PrintConsole();
            //Console.WriteLine();

            //Console.WriteLine("signature verification:" + rsa.VerifyDigitalSignature(sign, mesBytes, hashAlgorithm));




            //while (true)
            //{
            //    string text = Console.ReadLine();

            //    CryptographicHashAlgorithm hashAlgorithm = new MD_5();

            //    var hash = hashAlgorithm.GetHash(Encoding.UTF8.GetBytes(text));

            //    for (int i = 0; i < hash.Length; i++)
            //    {
            //        Console.Write(hash[i].ToString("x"));
            //    }
            //    Console.WriteLine();
            //}

        }
    }
}
