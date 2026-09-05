using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Referência ao plano da assinatura.</summary>
    public class PlanReference
    {
        /// <summary>Identificador do plano. Por exemplo, PLAN_123.</summary>
        public string? Id { get; set; }

        /// <summary>Nome do plano, devolvido pelo PagBank.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>Periodicidade do plano, devolvida pelo PagBank.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PlanInterval? Interval { get; set; }
    }
}
