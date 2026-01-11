using PagSeguro.DotNet.Sdk.Account.Models.Requests;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Responses
{
    public class CreatedAccountResponse : Shared.Account
    {
        public string? Id { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreateDate { get; set; }
        public TokenResponse? Token { get; set; }
        public CompanyRequest? Company { get; set; }
        [JsonPropertyName("tos_acceptance")]
        public TosAcceptanceRequest? TosAcceptance { get; set; }
    }
}
