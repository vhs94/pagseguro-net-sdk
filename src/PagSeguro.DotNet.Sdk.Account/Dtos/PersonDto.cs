using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Common.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class PersonDto
    {
        [JsonPropertyName("birth_date")]
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime BirthDate { get; set; }
        public string? Name { get; set; }
        [JsonPropertyName("mother_name")]
        public string? MotherName { get; set; }
        [JsonPropertyName("tax_id")]
        public string? TaxId { get; set; }
        public AddressDto? Address { get; set; }
        public ICollection<PhoneDto> Phones { get; set; }

        public PersonDto() => Phones = [];
    }
}
