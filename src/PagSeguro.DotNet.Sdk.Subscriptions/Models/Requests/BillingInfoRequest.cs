using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests
{
    /// <summary>
    /// Dados enviados para alterar o meio de pagamento do assinante.
    /// <see href="https://developer.pagbank.com.br/reference/alterar-dados-de-pagamento-do-assinante">ler documentação</see>
    /// </summary>
    public class BillingInfoRequest
    {
        /// <summary>Tipo do meio de pagamento. Atualmente apenas CREDIT_CARD.</summary>
        public string? Type { get; set; }

        /// <summary>Novo cartão a ser usado nas cobranças recorrentes.</summary>
        public SubscriptionCard? Card { get; set; }
    }
}
