using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>Página de assinantes retornada pela listagem.</summary>
    public class CustomerListResponse
    {
        /// <summary>Informações de paginação.</summary>
        [JsonPropertyName("result_set")]
        public ResultSet? ResultSet { get; set; }

        /// <summary>Assinantes encontrados.</summary>
        public ICollection<CustomerResponse> Customers { get; set; } = [];
    }
}
