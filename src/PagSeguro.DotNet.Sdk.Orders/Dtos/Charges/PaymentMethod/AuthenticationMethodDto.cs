using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public class AuthenticationMethodDto
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Cavv { get; set; }
        public string Eci { get; set; }
        public string Xid { get; set; }
        public string Version { get; set; }
        [JsonProperty("dstrans_id")]
        public string DstransId { get; set; }
    }
}