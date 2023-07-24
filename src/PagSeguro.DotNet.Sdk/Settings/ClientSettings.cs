using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Settings
{
    public class ClientSettings
    {
        public PagSeguroEnvironment Environment { get; set; }
        public string Token { get; set; }
        public string PrivateKey { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
