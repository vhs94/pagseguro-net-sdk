using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public class CreatedAccountDto : AccountWriteDto
    {
        public string? Id { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreateDate { get; set; }
        public TokenDto? Token { get; set; }
    }
}
