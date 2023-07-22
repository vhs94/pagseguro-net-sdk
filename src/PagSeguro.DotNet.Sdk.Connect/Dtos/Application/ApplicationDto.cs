using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Connect.Dtos.Application
{
    public abstract class ApplicationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Site { get; set; }
        [JsonProperty("redirect_uri")]
        public string RedirectUrl { get; set; }
    }
}
