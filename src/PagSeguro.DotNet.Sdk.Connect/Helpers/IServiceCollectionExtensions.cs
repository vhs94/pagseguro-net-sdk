using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Providers;
using PagSeguro.DotNet.Sdk.Connect.Services;

namespace PagSeguro.DotNet.Sdk.Connect.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddConnectClient(this IServiceCollection services)
        {
            services.AddScoped<IApplicationProvider, ApplicationProvider>();
            services.AddScoped<IAuthorizationProvider, AuthorizationProvider>();
            services.AddScoped<ICryptoService, CryptoService>();
        }
    }
}
