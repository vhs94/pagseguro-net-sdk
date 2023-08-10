using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Converters;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    [JsonConverter(typeof(CreditCardBrandDtoConverter))]
    public class CreditCardDto
    {
        public CreditCardBrandDto Brand { get; set; }
    }
}