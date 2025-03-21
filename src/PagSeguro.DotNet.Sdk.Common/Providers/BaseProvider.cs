using Flurl;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Common.Providers
{
    public abstract class BaseProvider(PagSeguroSettings settings) : IProvider
    {
        public PagSeguroSettings Settings { get; set; } = settings;
        public Url BaseUrl => Settings.Environment == PagSeguroEnvironment.Sandbox
            ? CommonEndpoints.SandboxBaseUrl
            : CommonEndpoints.ProductionBaseUrl;
    }
}
