namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount
{
    public class SummaryDto
    {
        public int Total { get; set; }
        public int Paid { get; set; }
        public int Refunded { get; set; }
    }
}