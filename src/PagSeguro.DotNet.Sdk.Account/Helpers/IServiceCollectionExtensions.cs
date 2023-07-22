using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Account.Interfaces;
using PagSeguro.DotNet.Sdk.Account.Providers;

namespace PagSeguro.DotNet.Sdk.Account.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAccountClient(this IServiceCollection services)
        {
            services.AddScoped<IAccountProvider, AccountProvider>();
        }
    }
}
