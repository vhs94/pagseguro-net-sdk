using PagSeguro.DotNet.Sdk.Account.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Requests
{
    public class AccountRequest : Shared.Account
    {
        public CompanyRequest? Company { get; set; }
        [JsonPropertyName("tos_acceptance")]
        public TosAcceptanceRequest? TosAcceptance { get; set; }
    }
}
