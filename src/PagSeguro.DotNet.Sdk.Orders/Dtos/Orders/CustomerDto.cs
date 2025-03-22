using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public class CustomerDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        [JsonPropertyName("tax_id")]
        public string? TaxId { get; set; }
        public ICollection<PhoneDto> Phones { get; set; }

        public CustomerDto() => Phones = [];
    }
}
