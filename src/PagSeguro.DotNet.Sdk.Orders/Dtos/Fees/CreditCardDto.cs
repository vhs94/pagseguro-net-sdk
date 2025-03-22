using PagSeguro.DotNet.Sdk.Orders.Converters;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Fees
{
    [JsonConverter(typeof(CreditCardBrandDtoConverter))]
    public class CreditCardDto
    {
        public CreditCardBrandDto? Brand { get; set; }
    }
}