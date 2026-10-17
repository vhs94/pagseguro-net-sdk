using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Cards;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Splits;
using PagSeguro.DotNet.Sdk.Orders.Providers.Cards;
using PagSeguro.DotNet.Sdk.Orders.Providers.Charges;
using PagSeguro.DotNet.Sdk.Orders.Providers.Fees;
using PagSeguro.DotNet.Sdk.Orders.Providers.Orders;
using PagSeguro.DotNet.Sdk.Orders.Providers.Splits;

namespace PagSeguro.DotNet.Sdk.Orders.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddOrderClient(this IServiceCollection services)
        {
            services.AddTransient<IOrderProvider, OrderProvider>();
            services.AddTransient<IChargeWithPaymentMethodProvider, ChargeWithPaymentMethodProvider>();
            services.AddTransient<IBankSlipChargeProvider, BankSlipChargeProvider>();
            services.AddTransient<ICreditCardChargeProvider, CreditCardChargeProvider>();
            services.AddTransient<ICreditCardWith3DsAuthChargeProvider, CreditCardWith3DsAuthChargeProvider>();
            services.AddTransient<IDebitCardWith3DsAuthChargeProvider, DebitCardWith3DsAuthChargeProvider>();
            services.AddTransient<IBankSlipOrderProvider, BankSlipOrderProvider>();
            services.AddTransient<ICreditCardOrderProvider, CreditCardOrderProvider>();
            services.AddTransient<ICreditCardWith3DsAuthOrderProvider, CreditCardWith3DsAuthOrderProvider>();
            services.AddTransient<IDebitCardWith3DsAuthOrderProvider, DebitCardWith3DsAuthOrderProvider>();
            services.AddTransient<IFeeProvider, FeeProvider>();
            services.AddTransient<ISplitProvider, SplitProvider>();
            services.AddTransient<ICardTokenProvider, CardTokenProvider>();
            services.AddTransient<IAuthenticationSessionProvider, AuthenticationSessionProvider>();
            services.AddAutoMapper(typeof(OrderProvider));
        }
    }
}
