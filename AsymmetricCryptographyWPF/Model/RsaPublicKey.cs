using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsymmetricCryptographyWPF.Model
{
    class RsaPublicKey
    {
        public int Id { get; set; }
        public string Modulus { get; set; }
        public string PublicExponent { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }
    }
}
