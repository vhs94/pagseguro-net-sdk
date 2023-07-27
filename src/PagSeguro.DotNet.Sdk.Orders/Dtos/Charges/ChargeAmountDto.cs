namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class ChargeAmountDto
    {
        public int Value { get; set; }
        public string Currency { get; set; }
        public SummaryDto Summary { get; set; }
    }
}