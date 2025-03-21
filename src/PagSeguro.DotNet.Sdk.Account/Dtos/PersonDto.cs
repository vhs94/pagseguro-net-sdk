using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Common.Converters;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class PersonDto
    {
        [JsonProperty("birth_date")]
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime BirthDate { get; set; }
        public string? Name { get; set; }
        [JsonProperty("mother_name")]
        public string? MotherName { get; set; }
        [JsonProperty("tax_id")]
        public string? TaxId { get; set; }
        public AddressDto? Address { get; set; }
        public ICollection<PhoneDto> Phones { get; set; }

        public PersonDto() => Phones = [];
    }
}
