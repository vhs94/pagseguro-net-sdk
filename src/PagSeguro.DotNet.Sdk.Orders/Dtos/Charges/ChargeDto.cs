using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges
{
    public abstract class ChargeDto
    {
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }
        public string Description { get; set; }
        public ICollection<string> NotificationUrls { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public ICollection<LinkDto> Links { get; set; }

        public ChargeDto()
        {
            NotificationUrls = new List<string>();
            Metadata = new Dictionary<string, string>();
            Links = new List<LinkDto>();
        }
    }
}
