using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard
{
    public class CreditCardPaymentMethodWriteDto : CreditCardPaymentMethodDto
    {
        public CardWriteDto Card { get; set; }
    }
}
