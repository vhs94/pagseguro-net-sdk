using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Ciclo de faturamento corrente da assinatura.</summary>
    public class BillingCycle
    {
        /// <summary>Número da ocorrência do ciclo.</summary>
        public int Occurrence { get; set; }
    }
}
