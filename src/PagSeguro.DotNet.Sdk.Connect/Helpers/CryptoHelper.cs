using System.Security.Cryptography;
using System.Text;

namespace PagSeguro.DotNet.Sdk.Connect.Helpers
{
    public static class CryptoHelper
    {
        public static string DecryptRsa(string privateKey, string encryptedContent)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
            var decrypted = rsa.Decrypt(
                Convert.FromBase64String(encryptedContent),
                RSAEncryptionPadding.OaepSHA256);
            return Encoding.Default.GetString(decrypted);
        }
    }
}
