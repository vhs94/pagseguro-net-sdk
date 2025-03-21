using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class AccountWriteDto : AccountDto
    {
        public CompanyWriteDto? Company { get; set; }
        [JsonProperty("tos_acceptance")]
        public TosAcceptance? TosAcceptance { get; set; }
    }
}
