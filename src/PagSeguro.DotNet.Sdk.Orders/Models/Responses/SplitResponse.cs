using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Divisão de pagamento de um pedido.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-divisao-do-pagamento">ler documentação</see>
    /// </summary>
    public class SplitResponse
    {
        /// <summary>Identificador da divisão. Por exemplo, SPLI_123.</summary>
        public string? Id { get; set; }

        /// <summary>
        /// Como os valores dos recebedores foram interpretados. Valores
        /// possíveis: FIXED e PERCENTAGE.
        /// </summary>
        public string? Method { get; set; }

        /// <summary>Recebedores da divisão, incluindo a conta principal.</summary>
        public ICollection<SplitReceiverResponse> Receivers { get; set; } = [];

        /// <summary>Links relacionados à divisão.</summary>
        public ICollection<Link> Links { get; set; } = [];
    }
}
