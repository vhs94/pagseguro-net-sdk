using PagSeguro.DotNet.Sdk.Account.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Requests
{
    public class CompanyRequest : Company
    {
        [JsonPropertyName("name")]
        public string? CompanyName { get; set; }
        public Address? Address { get; set; }
    }
}
