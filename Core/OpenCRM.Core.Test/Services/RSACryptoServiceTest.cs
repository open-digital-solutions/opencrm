using OpenDHS.Shared.Services;

namespace OpenDHS.Shared.Test.Services
{
    [TestClass]
    public class RSACryptoServiceTest
    {

        [TestMethod]
        public void GenerateKeys()
        {
            var rsaKeyPairs = RSACryptoService.GetKeyPairs();
            if (rsaKeyPairs == null) Assert.Fail("RSAKeyPair is null");
        }

        [TestMethod]
        public void EncryptionDecryption()
        {
            var rsaKeyPairs = RSACryptoService.GetKeyPairs();
            var textToEncrypt = "I will be encrypted!";
            var encryptedText = RSACryptoService.Encrypt(textToEncrypt, rsaKeyPairs.PublicKey);
            var decryptedText = RSACryptoService.Decrypt(encryptedText, rsaKeyPairs.PrivateKey);

            Assert.AreEqual(textToEncrypt, decryptedText);
        }
    }
}