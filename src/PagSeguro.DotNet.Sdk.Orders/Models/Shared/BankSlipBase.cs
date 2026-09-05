using PagSeguro.DotNet.Sdk.Common.Serialization;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dados comuns do boleto gerado para a cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public abstract class BankSlipBase
    {
        /// <summary>
        /// Data de vencimento do boleto.
        /// </summary>
        [JsonPropertyName("due_date")]
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime DueDate { get; set; }
        /// <summary>
        /// Linhas de instrução impressas no boleto.
        /// </summary>
        [JsonPropertyName("instruction_lines")]
        public InstructionLines? InstructionLines { get; set; }
        /// <summary>
        /// Dados do responsável pelo pagamento do boleto.
        /// </summary>
        public BankSlipHolder? Holder { get; set; }
    }
}
