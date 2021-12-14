using AsymmetricCryptography.Core.NumbersGenerating;
using AsymmetricCryptography.Core.PrimalityVerification;
using System.Numerics;
using AsymmetricCryptography.Core;
using AsymmetricCryptography.Core.HashAlgorithms;
using System.Text;

//var dp1 = new DsaDomainParameter(1, 1, 1, 1)
//{
//    Name = "dp1"
//};

//var dp2 = new DsaDomainParameter(2, 2, 2, 2)
//{
//    Name = "dp2"
//};

//var q1 = new DsaPrivateKey(25, 1, dp1)
//{
//    Name = "q1"
//};

//var q2 = new DsaPrivateKey(20, 2, dp2)
//{
//    Name = "q2"
//};
//var q3 = new DsaPrivateKey(14, 3, dp1)
//{
//    Name = "q3"
//};

//using(var context=new KeysContext())
//{
//    context.Keys.Add(dp1);
//    context.Keys.Add(dp2);

//    context.Keys.Add(q1);
//    context.Keys.Add(q2);
//    context.Keys.Add(q3);

//    context.SaveChanges();
//}

//List<DsaDomainParameter> dpList = new List<DsaDomainParameter>();
//List<DsaPrivateKey> keyList = new List<DsaPrivateKey>();

//using (var db=new KeysContext())
//{
//    foreach(var el in db.DsaPrivateKeys)
//    {
//        keyList.Add(el);
//    }

//    var zhopa = keyList[0].DomainParameter;

//    //var w = keyList[0].KeyType.GetType();
//    Console.WriteLine(zhopa.GetType());
//    //Console.WriteLine(w == typeof(string));
//    //Console.WriteLine(w == typeof(KeyType));
//    //Console.WriteLine(keyList[0].KeyType);
//    //Console.WriteLine(keyList[0].KeyType.GetType());
//    //foreach (var item in db.DsaDomainParameters)
//    //{
//    //    dpList.Add(item);
//    //}
//}

//PrimalityVerificator w = new MillerRabinPrimalityVerificator();
//NumberGenerator q = new FibonacciNumberGenerator();

//int s = 0;
//for (int i = 1; i < 100; i++)
{
    //if(i%1000==0)
    //  Console.WriteLine($"\t{i}");
    //BigInteger num = i;

    //var s = num.GetBitLength();
    // var d = num.GetBinaryLength();

    // Console.WriteLine($"{s}  {d}");

    //BigInteger n = i;

    //Console.WriteLine($"{n}:{w.IsPrime(n)}");

    //BigInteger min = 1274617846;
    //BigInteger max = 12746178462355;
    //BigInteger num = q.GenerateNumber(min, max);

    //if (num <= min+10 || num >= max-10)
    //    Console.WriteLine("PEZDEC");
    //else
    //    s++;

    //BigInteger num = i;

    //if (w.IsPrime(num))
    //    Console.WriteLine(num);
}

//byte[] data = new byte[54];

//for (int i = 0; i < data.Length; i++)
//{
//    data[i] = 255;
//}

//var q = HashAlgorithm.Preprocess(data, 64, 1, 8);

//for (int i = 0; i < q.Length; i++)
//{
//    Console.WriteLine(Convert.ToString(q[i], 2).PadLeft(8, '0'));
//}

string str = "zhopa";
HashAlgorithm q = new MD_5();
//while (true)
{
    //str = Console.ReadLine();

    var bytes = Encoding.UTF8.GetBytes(str);

    var has = q.GetHash(bytes);

    string hh = "";

    for (int i = 0; i < has.Length; i++)
    {
        hh += Convert.ToString(has[i], 16);
    }

    Console.WriteLine(hh);
}

