using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>Página de estornos retornada pela listagem.</summary>
    public class RefundListResponse
    {
        /// <summary>Informações de paginação.</summary>
        [JsonPropertyName("result_set")]
        public ResultSet? ResultSet { get; set; }

        /// <summary>Estornos encontrados.</summary>
        public ICollection<RefundResponse> Refunds { get; set; } = [];
    }
}
