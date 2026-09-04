using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Linhas de instrução exibidas no boleto.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public class InstructionLines
    {
        /// <summary>
        /// Primeira linha de instruções. De 1 a 75 caracteres.
        /// </summary>
        [JsonPropertyName("line_1")]
        public string? FirstLine { get; set; }
        /// <summary>
        /// Segunda linha de instruções. De 1 a 75 caracteres.
        /// </summary>
        [JsonPropertyName("line_2")]
        public string? SecondLine { get; set; }
    }
}
