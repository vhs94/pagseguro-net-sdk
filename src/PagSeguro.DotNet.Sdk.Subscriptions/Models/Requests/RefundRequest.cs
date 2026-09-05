using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests
{
    /// <summary>
    /// Dados enviados para estornar um pagamento de assinatura.
    /// <see href="https://developer.pagbank.com.br/reference/criar-estorno-de-pagamento">ler documentação</see>
    /// </summary>
    public class RefundRequest
    {
        /// <summary>Valor a ser estornado. Permite estorno parcial.</summary>
        public Money? Amount { get; set; }
    }
}
