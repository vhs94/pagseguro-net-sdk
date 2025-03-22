using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod
{
    public abstract class PaymentMethodDto(PaymentMethodType type)
    {
        public string Type { get; } = type.ToDescription();
    }
}