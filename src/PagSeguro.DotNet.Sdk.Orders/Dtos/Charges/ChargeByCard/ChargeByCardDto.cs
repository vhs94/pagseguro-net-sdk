namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByCard
{
    public abstract class ChargeByCardDto : ChargeDto
    {
        public IDictionary<string, string> Metadata { get; set; }

        public ChargeByCardDto()
        {
            Metadata = new Dictionary<string, string>();
        }
    }
}
