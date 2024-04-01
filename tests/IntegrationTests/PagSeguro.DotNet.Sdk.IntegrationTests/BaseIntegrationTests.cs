using AutoFixture;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Settings;

namespace PagSeguro.DotNet.Sdk.IntegrationTests
{
    public abstract class BaseIntegrationTests
    {
        public string PublicKey { get; }
        public ClientSettings Settings { get; }
        public PagSeguroClient Client { get; }
        public Fixture Fixture { get; }

        protected BaseIntegrationTests()
        {
            PublicKey = File.ReadAllText("Assets/public-key.txt");
            Settings = CreateSettings();
            Client = CreatePagSeguroClient();
            Fixture = new Fixture();
        }

        private ClientSettings CreateSettings()
        {
            return new ClientSettings
            {
                ClientId = "8815a5e2-616b-4754-a6d1-40d92b71674c",
                ClientSecret = "45e5a4de-b8eb-4b78-9ce6-fad416b1953c",
                Token = "BCABE5E7AA9D43BCBBB76E3C45C1567A",
                PrivateKey = File.ReadAllText("Assets/private-key.txt"),
                Environment = PagSeguroEnvironment.Sandbox
            };
        }

        private PagSeguroClient CreatePagSeguroClient()
        {
            return new PagSeguroClient(Settings);
        }
    }
}
