using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class CardDto
    {
        public string Id { get; set; }
        public string Number { get; set; }
        [JsonProperty("network_token")]
        public string NetworkToken { get; set; }
        [JsonProperty("exp_month")]
        public int ExpMonth { get; set; }
        [JsonProperty("exp_year")]
        public int ExpYear { get; set; }
        [JsonProperty("security_code")]
        public int SecurityCode { get; set; }
        public bool Store { get; set; }
        public string Brand { get; set; }
        public string Product { get; set; }
        [JsonProperty("first_digits")]
        public int FirstDigits { get; set; }
        [JsonProperty("last_digits")]
        public int LastDigits { get; set; }
        public HolderDto Holder { get; set; }
    }
}