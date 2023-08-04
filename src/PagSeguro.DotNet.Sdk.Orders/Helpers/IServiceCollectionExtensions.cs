using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddOrderClient(this IServiceCollection services)
        {
            services.AddTransient<IOrderProvider, OrderProvider>();
            services.AddTransient<IChargeProvider, ChargeProvider>();
            services.AddTransient<IChargeByBankSlipProvider, ChargeByBankSlipProvider>();
            services.AddTransient<IChargeByCreditCardProvider, ChargeByCreditCardProvider>();
            services.AddTransient<IChargeByCreditCardWith3DsAuthProvider, ChargeByCreditCardWith3DsAuthProvider>();
            services.AddTransient<IChargeByDebitCardWith3DsAuthProvider, ChargeByDebitCardWith3DsAuthProvider>();
            services.AddTransient(typeof(IGenericOrderProvider<,>), typeof(GenericOrderProvider<,>));
        }
    }
}
