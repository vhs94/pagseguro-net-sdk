using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class AccountWriteDto : AccountDto
    {
        public CompanyWriteDto? Company { get; set; }
        [JsonPropertyName("tos_acceptance")]
        public TosAcceptanceDto? TosAcceptance { get; set; }
    }
}
