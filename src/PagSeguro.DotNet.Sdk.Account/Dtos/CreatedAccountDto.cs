using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class CreatedAccountDto : AccountWriteDto
    {
        public string Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreateDate { get; set; }
        public TokenDto Token { get; set; }
    }
}
