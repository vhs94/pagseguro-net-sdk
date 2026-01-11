using PagSeguro.DotNet.Sdk.Account.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Responses
{
    public class CompanyResponse : Company
    {
        [JsonPropertyName("company_name")]
        public string? CompanyName { get; set; }
        public ICollection<Address> Address { get; set; }

        public CompanyResponse() => Address = [];
    }
}
