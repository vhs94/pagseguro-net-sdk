using Flurl;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Settings;

namespace PagSeguro.DotNet.Sdk.Common.Providers
{
    public abstract class BaseProvider : IProvider
    {
        public PagSeguroSettings Settings { get; set; }
        public Url BaseUrl => Settings.Environment == PagSeguroEnvironment.Sandbox
            ? CommonEndpoints.SandboxBaseUrl
            : CommonEndpoints.ProductionBaseUrl;

        protected BaseProvider(PagSeguroSettings settings)
        {
            Settings = settings;
        }
    }
}
