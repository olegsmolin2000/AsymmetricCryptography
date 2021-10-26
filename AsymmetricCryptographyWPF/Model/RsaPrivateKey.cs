using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsymmetricCryptographyWPF.Model
{
    class RsaPrivateKey
    {
        public int Id { get; set; }
        public string Modulus { get; set; }
        public string PrivateExponent { get; set; }
    }
}
