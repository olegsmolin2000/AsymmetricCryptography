using AsymmetricCryptography.DataUnits.Keys;
using AsymmetricCryptography.DataUnits.Keys.DSA;
using AsymmetricCryptography.EFCore.Context;
using Microsoft.EntityFrameworkCore;

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
List<DsaPrivateKey> keyList = new List<DsaPrivateKey>();

using (var db=new KeysContext())
{
    foreach(var el in db.DsaPrivateKeys.Include(key=>key.DomainParameter))
    {
        keyList.Add(el);
    }

    var w = keyList[0].KeyType.GetType();

    Console.WriteLine(w == typeof(string));
    Console.WriteLine(w == typeof(KeyType));
    Console.WriteLine(keyList[0].KeyType);
    Console.WriteLine(keyList[0].KeyType.GetType());
    //foreach (var item in db.DsaDomainParameters)
    //{
    //    dpList.Add(item);
    //}




}