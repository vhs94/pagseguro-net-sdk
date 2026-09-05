using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Meio de pagamento com boleto enviado na cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public class BankSlipPaymentMethodRequest : BankSlipPaymentMethodBase
    {
        /// <summary>
        /// Dados do boleto a ser gerado.
        /// </summary>
        [JsonPropertyName("boleto")]
        public BankSlipRequest? BankSlip { get; set; }
    }
}
