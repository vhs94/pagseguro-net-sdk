using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>Página de assinaturas retornada pela listagem.</summary>
    public class SubscriptionListResponse
    {
        /// <summary>Informações de paginação.</summary>
        [JsonPropertyName("result_set")]
        public ResultSet? ResultSet { get; set; }

        /// <summary>Assinaturas encontradas.</summary>
        public ICollection<SubscriptionResponse> Subscriptions { get; set; } = [];
    }
}
