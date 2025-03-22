using FluentAssertions;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Services;

namespace PagSeguro.DotNet.Sdk.Connect.Tests.Services
{
    public class CryptoServiceTests
    {
        private readonly ICryptoService _service;
        private readonly string _encryptedChallenge;
        private readonly string _decryptedChallenge;

        public CryptoServiceTests()
        {
            _service = CreateService();
            _encryptedChallenge = File.ReadAllText("Assets/encrypted-challenge.txt");
            _decryptedChallenge = File.ReadAllText("Assets/decrypted-challenge.txt");
        }

        private static ICryptoService CreateService()
        {
            PagSeguroSettings settings = CreateSettings();
            return new CryptoService(settings);
        }

        private static PagSeguroSettings CreateSettings()
        {
            return new PagSeguroSettings
            {
                PrivateKey = File.ReadAllText("Assets/private-key.txt")
            };
        }

        [Fact]
        public void Decrypt_ContentAndPrivateKeyAreValid_ContentIsDecrypted()
        {
            string decryptedChallenge = _service.Decrypt(_encryptedChallenge);

            decryptedChallenge
                .Should()
                .Be(_decryptedChallenge);
        }
    }
}
