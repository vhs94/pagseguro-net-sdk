using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>Página de faturas retornada pela listagem.</summary>
    public class InvoiceListResponse
    {
        /// <summary>Informações de paginação.</summary>
        [JsonPropertyName("result_set")]
        public ResultSet? ResultSet { get; set; }

        /// <summary>Faturas encontradas.</summary>
        public ICollection<InvoiceResponse> Invoices { get; set; } = [];
    }
}
