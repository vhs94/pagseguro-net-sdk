using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Connect.Interfaces;
using PagSeguro.DotNet.Sdk.Connect.Providers;

namespace PagSeguro.DotNet.Sdk.Connect.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddConnectClient(this IServiceCollection services)
        {
            services.AddScoped<IApplicationProvider, ApplicationProvider>();
            services.AddScoped<IAuthorizationProvider, AuthorizationProvider>();
        }
    }
}
