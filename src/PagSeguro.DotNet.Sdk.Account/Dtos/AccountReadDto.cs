using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class AccountReadDto : AccountDto
    {
        public string Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public CompanyReadDto Company { get; set; }
    }
}
