using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Common.Dtos
{
    public class BadRequestResponseDto
    {
        [JsonProperty("error_messages")]
        public ICollection<BadRequestMessageDto> ErrorMessages { get; set; }

        public BadRequestResponseDto()
        {
            ErrorMessages = new List<BadRequestMessageDto>();
        }
    }
}
