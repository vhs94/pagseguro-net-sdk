using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard
{
    public class CreditCardPaymentMethodReadDto : CreditCardPaymentMethodDto
    {
        public CardReadDto? Card { get; set; }
    }
}
