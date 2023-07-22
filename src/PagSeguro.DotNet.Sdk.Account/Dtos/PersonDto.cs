using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class PersonDto
    {
        [JsonProperty("birth_date")]
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        [JsonProperty("tax_id")]
        public string TaxId { get; set; }
        public ICollection<PhoneDto> Phones { get; set; }

        public PersonDto()
        {
            Phones = new List<PhoneDto>();
        }
    }
}
