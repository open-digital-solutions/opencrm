using OpenCRM.Core.Services;

namespace OpenCRM.Core.Test.Services
{
    [TestClass]
    public class RSACryptoServiceTest
    {

        [TestMethod]
        public void GenerateKeys()
        {
            var rsaKeyPairs = RSACryptoService.GetCypherDataModel();
            if (rsaKeyPairs == null) Assert.Fail("RSAKeyPair is null");
        }

        [TestMethod]
        public void EncryptionDecryption()
        {
            var rsaKeyPairs = RSACryptoService.GetCypherDataModel();
            var textToEncrypt = "I will be encrypted!";
            var encryptedText = RSACryptoService.Encrypt(textToEncrypt, rsaKeyPairs.PublicKey);
            var decryptedText = RSACryptoService.Decrypt(encryptedText, rsaKeyPairs.PrivateKey);

            Assert.AreEqual(textToEncrypt, decryptedText);
        }
    }
}