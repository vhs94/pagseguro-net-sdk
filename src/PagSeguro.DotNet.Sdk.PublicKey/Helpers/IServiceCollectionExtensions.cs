using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.PublicKey.Interfaces;
using PagSeguro.DotNet.Sdk.PublicKey.Providers;

namespace PagSeguro.DotNet.Sdk.PublicKey.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAPublicKeyClient(this IServiceCollection services)
        {
            services.AddScoped<IPublicKeyProvider, PublicKeyProvider>();
        }
    }
}
