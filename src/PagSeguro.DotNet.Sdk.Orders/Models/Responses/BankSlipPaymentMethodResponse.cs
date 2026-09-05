using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Meio de pagamento com boleto retornado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public class BankSlipPaymentMethodResponse : BankSlipPaymentMethodBase
    {
        /// <summary>
        /// Dados do boleto gerado.
        /// </summary>
        [JsonPropertyName("boleto")]
        public BankSlipResponse? BankSlip { get; set; }
    }
}
