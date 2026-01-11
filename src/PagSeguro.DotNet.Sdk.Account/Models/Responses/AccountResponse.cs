using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Responses
{
    public class AccountResponse : Shared.Account
    {
        public string? Id { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreateDate { get; set; }
        public string? Status { get; set; }
        public CompanyResponse? Company { get; set; }
    }
}
