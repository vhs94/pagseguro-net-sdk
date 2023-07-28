namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public abstract class ChargeAmountDto
    {
        public int Value { get; set; }
        public string Currency { get; set; }
    }
}