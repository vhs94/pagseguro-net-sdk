using PagSeguro.DotNet.Sdk.Common.Attributes;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PagSeguro.DotNet.Sdk.Connect.Services
{
    public class CryptoService(PagSeguroSettings settings) : ICryptoService
    {

        [PrivateKeyRequired]
        public string Decrypt(string encryptedContent)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(settings.PrivateKey!), out _);
            var decrypted = rsa.Decrypt(
                Convert.FromBase64String(encryptedContent),
                RSAEncryptionPadding.OaepSHA256);
            return Encoding.Default.GetString(decrypted);
        }
    }
}
