using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card
{
    public abstract class CardDto
    {
        [JsonPropertyName("exp_month")]
        public int ExpMonth { get; set; }
        [JsonPropertyName("exp_year")]
        public int ExpYear { get; set; }
        public HolderDto? Holder { get; set; }
    }
}