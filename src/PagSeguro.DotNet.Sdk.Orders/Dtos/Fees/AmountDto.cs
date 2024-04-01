namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    public class AmountDto
    {
        public int Value { get; set; }
        public string Currency { get; set; }
        public FeesDto Fees { get; set; }
    }
}