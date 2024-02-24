using System.Security.Cryptography;
using System.Xml.Serialization;

namespace OpenCRM.Core.Crypto
{
    public class RSAKeyPairsModel
    {
        public RSAParameters PublicKey { get; set; }
        public RSAParameters PrivateKey { get; set; }


    }

    public static class RSAKeyPairsExtensions
    {
        public static string GetStringPublicKey(this RSAKeyPairsModel rsaKeyPairs)
        {
            var sw = new StringWriter();
            var xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(sw, rsaKeyPairs.PublicKey);
            return sw.ToString();
        }
        public static string GetStringPrivateKey(this RSAKeyPairsModel rsaKeyPairs)
        {
            var sw = new StringWriter();
            var xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(sw, rsaKeyPairs.PrivateKey);
            return sw.ToString();
        }
    }
}
