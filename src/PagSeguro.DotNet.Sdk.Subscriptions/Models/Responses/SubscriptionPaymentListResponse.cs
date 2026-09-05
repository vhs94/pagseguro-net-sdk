using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>Página de pagamentos retornada pela listagem.</summary>
    public class SubscriptionPaymentListResponse
    {
        /// <summary>Informações de paginação.</summary>
        [JsonPropertyName("result_set")]
        public ResultSet? ResultSet { get; set; }

        /// <summary>Pagamentos encontrados.</summary>
        public ICollection<SubscriptionPaymentResponse> Payments { get; set; } = [];
    }
}
