using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod
{
    public abstract class PaymentMethodDto
    {
        public string Type { get; }

        public PaymentMethodDto(PaymentMethodType type)
        {
            Type = type.ToDescription();
        }
    }
}