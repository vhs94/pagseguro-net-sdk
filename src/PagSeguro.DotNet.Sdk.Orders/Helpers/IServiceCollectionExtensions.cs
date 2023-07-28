using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Orders.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Providers;

namespace PagSeguro.DotNet.Sdk.Orders.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddOrderClient(this IServiceCollection services)
        {
            services.AddScoped<IOrderProvider, OrderProvider>();
            services.AddScoped<IChargeProvider, ChargeProvider>();
        }
    }
}
