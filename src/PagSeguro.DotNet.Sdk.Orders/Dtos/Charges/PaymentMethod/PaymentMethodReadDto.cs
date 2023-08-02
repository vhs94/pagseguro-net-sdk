using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod
{
    public class PaymentMethodReadDto : PaymentMethodDto
    {
        public string Type { get; set; }
        public CardReadDto Card { get; set; }
    }
}
