using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card
{
    public abstract class CardDto
    {
        [JsonProperty("exp_month")]
        public int ExpMonth { get; set; }
        [JsonProperty("exp_year")]
        public int ExpYear { get; set; }
        public HolderDto Holder { get; set; }
    }
}