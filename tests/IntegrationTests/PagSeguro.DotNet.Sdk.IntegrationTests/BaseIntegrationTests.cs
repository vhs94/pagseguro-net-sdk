using AutoFixture;
using Microsoft.Playwright;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Settings;

namespace PagSeguro.DotNet.Sdk.IntegrationTests
{
    public abstract class BaseIntegrationTests
    {
        public PagSeguroClient Client { get; }
        public Fixture Fixture { get; }

        public BaseIntegrationTests()
        {
            Client = CreatePagSeguroClient();
            Fixture = new Fixture();
        }

        private static PagSeguroClient CreatePagSeguroClient()
        {
            return new PagSeguroClient(new ClientSettings
            {
                ClientId = "8815a5e2-616b-4754-a6d1-40d92b71674c",
                ClientSecret = "45e5a4de-b8eb-4b78-9ce6-fad416b1953c",
                Token = "BCABE5E7AA9D43BCBBB76E3C45C1567A",
                PrivateKey = File.ReadAllText("Assets/private-key.txt"),
                Environment = PagSeguroEnvironment.Sandbox
            });
        }
        protected async Task ConnectAsync()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://acesso.pagseguro.uol.com.br/sandbox");
        }
    }
}
