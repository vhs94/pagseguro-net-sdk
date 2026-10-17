using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Recebedor de uma divisão de pagamento já processada.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-divisao-do-pagamento">ler documentação</see>
    /// </summary>
    public class SplitReceiverResponse
    {
        /// <summary>Pagamento gerado para o recebedor.</summary>
        public SplitPaymentReference? Payment { get; set; }

        /// <summary>Conta que recebeu a parcela.</summary>
        public SplitAccount? Account { get; set; }

        /// <summary>Valor destinado ao recebedor, em centavos.</summary>
        public SplitAmount? Amount { get; set; }

        /// <summary>
        /// Papel do recebedor na divisão. Valores possíveis: PRIMARY e SECONDARY.
        /// </summary>
        public string? Type { get; set; }
    }
}
