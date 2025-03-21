using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class TosAcceptance
    {
        [JsonProperty("user_ip")]
        public string? UserIp { get; set; }
        public DateTime Date { get; set; }
    }
}
