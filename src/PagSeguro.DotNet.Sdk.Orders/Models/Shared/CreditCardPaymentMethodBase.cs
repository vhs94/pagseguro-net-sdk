using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns do pagamento com cartão de crédito.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-cartao">ler documentação</see>
    /// </summary>
    public abstract class CreditCardPaymentMethodBase : CardPaymentMethodBase
    {
        /// <summary>
        /// Nome exibido na fatura do cliente. Até 22 caracteres,
        /// sem caracteres especiais. Disponível apenas para cartão de crédito.
        /// </summary>
        [JsonPropertyName("soft_descriptor")]
        public string? SoftDescriptor { get; set; }

        public CreditCardPaymentMethodBase()
            : base(PaymentMethodType.CreditCard)
        {
        }
    }
}
