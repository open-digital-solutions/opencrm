using OpenCRM.Core.Crypto;
using System.Security.Cryptography;
using System.Text;

namespace OpenCRM.Core.Services
{

    public class RSACryptoService
    {

        public RSACryptoService(string publicXmlKey, string privateXmlKey)
        {
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(2048);
            rsaCryptoServiceProvider.FromXmlString(publicXmlKey);
        }

        public static RSADataModel GetCypherDataModel() {

            var rsaKeyPairs = new RSADataModel();
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(2048);
            rsaKeyPairs.PublicKey = rsaCryptoServiceProvider.ExportParameters(false);
            rsaKeyPairs.PrivateKey = rsaCryptoServiceProvider.ExportParameters(true);

            rsaKeyPairs.UserKey = Encrypt(GetRandomString(), rsaKeyPairs.PublicKey);

            return rsaKeyPairs;
         }

      
        public static string Encrypt(string textToEncrypt, RSAParameters publicKey) {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
            
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(2048);
            rsaCryptoServiceProvider.ImportParameters(publicKey);

            var encryptedData = rsaCryptoServiceProvider.Encrypt(bytesToEncrypt, true);
            var base64Encrypted = Convert.ToBase64String(encryptedData);

            return base64Encrypted;
        }

        public static string Decrypt(string textToDecrypt, RSAParameters privateKey)
        {
            var base64Encrypted = Convert.FromBase64String(textToDecrypt);

            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(2048);
            rsaCryptoServiceProvider.ImportParameters(privateKey);

            var decryptedData = rsaCryptoServiceProvider.Decrypt(base64Encrypted, true);

            return  Encoding.UTF8.GetString(decryptedData);
        }

        public static string GetRandomString()
        {
            byte[] random = new byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }

    }
}
