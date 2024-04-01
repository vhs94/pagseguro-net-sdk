using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    public class FeeWriteDto
    {
        public string PaymentMethods => PaymentMethodType.CreditCard.ToDescription();
        public int Value { get; set; }
        public int MaxInstallments { get; set; }
        public int MaxInstallmentsNoInterest { get; set; }
        public int CreditCardBin { get; set; }
    }
}
