namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class PaymentMethodReadDto : PaymentMethodDto
    {
        public CardReadDto Card { get; set; }
    }
}
