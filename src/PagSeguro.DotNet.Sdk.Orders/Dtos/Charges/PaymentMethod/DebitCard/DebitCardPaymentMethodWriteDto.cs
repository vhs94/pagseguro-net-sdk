using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.DebitCard
{
    public class DebitCardPaymentMethodWriteDto : DebitCardPaymentMethodDto
    {
        public CardWriteDto Card { get; set; }
    }
}
