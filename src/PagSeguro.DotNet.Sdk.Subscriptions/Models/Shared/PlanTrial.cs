using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Configuração do período de teste do plano.
    /// </summary>
    public class PlanTrial
    {
        /// <summary>Duração do período de teste, em dias.</summary>
        public int Days { get; set; }

        /// <summary>Indica se o período de teste está habilitado.</summary>
        public bool Enabled { get; set; }

        /// <summary>Indica se a taxa de adesão é retida durante o período de teste.</summary>
        [JsonPropertyName("hold_setup_fee")]
        public bool HoldSetupFee { get; set; }
    }
}
