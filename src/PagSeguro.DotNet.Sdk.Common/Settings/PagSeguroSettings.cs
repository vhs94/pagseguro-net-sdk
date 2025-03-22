namespace PagSeguro.DotNet.Sdk.Common.Settings
{
    public class PagSeguroSettings
    {
        public PagSeguroEnvironment Environment { get; set; }
        public string? Token { get; set; }
        public string? PrivateKey { get; set; }
        public string? AccessToken { get; set; }
        public string? Challenge { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
