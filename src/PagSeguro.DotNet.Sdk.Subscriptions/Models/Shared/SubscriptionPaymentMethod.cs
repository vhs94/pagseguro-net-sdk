using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Meio de pagamento usado na cobrança recorrente.</summary>
    public class SubscriptionPaymentMethod
    {
        /// <summary>Tipo do meio de pagamento. Por exemplo, CREDIT_CARD e BOLETO.</summary>
        public string? Type { get; set; }

        /// <summary>Cartão usado na cobrança recorrente.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SubscriptionCard? Card { get; set; }
    }
}
