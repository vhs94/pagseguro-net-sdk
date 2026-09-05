using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Subscriptions.Interfaces;
using PagSeguro.DotNet.Sdk.Subscriptions.Providers;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddSubscriptionsClient(this IServiceCollection services)
        {
            services.AddScoped<IPlanProvider, PlanProvider>();
            services.AddScoped<ICustomerProvider, CustomerProvider>();
            services.AddScoped<ISubscriptionProvider, SubscriptionProvider>();
            services.AddScoped<ICouponProvider, CouponProvider>();
            services.AddScoped<IInvoiceProvider, InvoiceProvider>();
            services.AddScoped<ISubscriptionPaymentProvider, SubscriptionPaymentProvider>();
            services.AddScoped<ISubscriptionPreferenceProvider, SubscriptionPreferenceProvider>();
        }
    }
}
