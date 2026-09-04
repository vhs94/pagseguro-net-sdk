using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Cobrança paga com boleto retornada pela API.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public class ChargeByBankSlipResponse : ChargeByBankSlipBase
    {
        /// <summary>
        /// Identificador da cobrança PagBank. 41 caracteres.
        /// Por exemplo, CHAR_67FC568B-00D8-431D-B2E7-755E3E6C66A0.
        /// </summary>
        public new string? Id { get; set; }
        /// <summary>
        /// Situação da cobrança.
        /// Valores possíveis: AUTHORIZED, PAID, IN_ANALYSIS, DECLINED, CANCELED e WAITING.
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// Data e horário em que foi criada a cobrança.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Valor da cobrança.
        /// </summary>
        public ChargeAmountResponse? Amount { get; set; }
        /// <summary>
        /// Resposta da autorização enviada pelo emissor.
        /// </summary>
        [JsonPropertyName("payment_response")]
        public PaymentResponse? PaymentResponse { get; set; }
        /// <summary>
        /// Meio de pagamento com boleto utilizado na cobrança.
        /// </summary>
        [JsonPropertyName("payment_method")]
        public BankSlipPaymentMethodResponse? PaymentMethod { get; set; }
        /// <summary>
        /// Links relacionados à cobrança.
        /// </summary>
        public ICollection<Link> Links { get; set; }

        public ChargeByBankSlipResponse() => Links = [];
    }
}
