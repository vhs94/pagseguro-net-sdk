using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Checkout.Interfaces;
using PagSeguro.DotNet.Sdk.Checkout.Providers;

namespace PagSeguro.DotNet.Sdk.Checkout.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCheckoutClient(this IServiceCollection services)
        {
            services.AddScoped<ICheckoutProvider, CheckoutProvider>();
        }
    }
}
