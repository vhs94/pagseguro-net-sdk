namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Meio de pagamento cadastrado para o assinante.</summary>
    public class BillingInfo
    {
        /// <summary>Tipo do meio de pagamento. Atualmente apenas CREDIT_CARD.</summary>
        public string? Type { get; set; }

        /// <summary>Cartão usado na cobrança recorrente.</summary>
        public SubscriptionCard? Card { get; set; }
    }
}
