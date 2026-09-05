using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Cobrança paga com cartão retornada pela API.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public abstract class ChargeByCardResponse : ChargeByCardBase
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
        /// Data e horário em que a cobrança foi paga (capturada).
        /// </summary>
        [JsonPropertyName("paid_at")]
        public DateTime? PaidDate { get; set; }
        /// <summary>
        /// Valor da cobrança.
        /// </summary>
        public ChargeAmountResponse? Amount { get; set; }
        /// <summary>
        /// Resposta da autorização enviada pelo emissor.
        /// </summary>
        [JsonPropertyName("payment_response")]
        public CardPaymentResponse? PaymentResponse { get; set; }
        /// <summary>
        /// Links relacionados à cobrança.
        /// </summary>
        public ICollection<Link> Links { get; set; }

        public ChargeByCardResponse() => Links = [];
    }
}
