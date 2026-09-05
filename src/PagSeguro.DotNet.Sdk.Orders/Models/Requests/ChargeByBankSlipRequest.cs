using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Cobrança paga com boleto.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public class ChargeByBankSlipRequest : ChargeByBankSlipBase, IChargeRequest
    {
        /// <summary>
        /// Valor da cobrança.
        /// </summary>
        public ChargeAmountRequest? Amount { get; set; }
        /// <summary>
        /// Meio de pagamento com boleto.
        /// </summary>
        [JsonPropertyName("payment_method")]
        public BankSlipPaymentMethodRequest? PaymentMethod { get; set; }
    }
}
