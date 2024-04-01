using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard
{
    public class DebitCardPaymentMethodReadDto : DebitCardPaymentMethodDto
    {
        public CardReadDto Card { get; set; }
    }
}
