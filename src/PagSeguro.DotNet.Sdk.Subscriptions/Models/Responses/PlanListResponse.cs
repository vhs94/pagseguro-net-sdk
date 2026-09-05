using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>
    /// Página de planos retornada pela listagem.
    /// <see href="https://developer.pagbank.com.br/reference/listar-planos">ler documentação</see>
    /// </summary>
    public class PlanListResponse
    {
        /// <summary>Informações de paginação.</summary>
        [JsonPropertyName("result_set")]
        public ResultSet? ResultSet { get; set; }

        /// <summary>Planos encontrados.</summary>
        public ICollection<PlanResponse> Plans { get; set; } = [];
    }
}
