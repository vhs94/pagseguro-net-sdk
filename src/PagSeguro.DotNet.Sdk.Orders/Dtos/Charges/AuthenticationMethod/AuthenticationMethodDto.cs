using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.AuthenticationMethod
{
    public abstract class AuthenticationMethodDto
    {
        public string? Type { get; set; }
        public string? Cavv { get; set; }
        public string? Xid { get; set; }
        public string? Eci { get; set; }
        public string? Version { get; set; }
        [JsonPropertyName("dstrans_id")]
        public string? DstransId { get; set; }
    }
}