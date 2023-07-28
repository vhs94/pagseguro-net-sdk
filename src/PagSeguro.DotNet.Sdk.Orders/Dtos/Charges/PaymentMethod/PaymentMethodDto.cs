namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public abstract class PaymentMethodDto
    {
        public string Type { get; set; }
        public int Installments { get; set; }
        public bool Capture { get; set; }
    }
}