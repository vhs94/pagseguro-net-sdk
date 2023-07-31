using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Common.Dtos
{
    public class BadRequestMessageDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        [JsonProperty("parameter_name")]
        public string ParameterName { get; set; }
    }
}
