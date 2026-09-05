using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Opção de parcelamento simulada para a transação.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    public class InstallmentPlan
    {
        /// <summary>
        /// Quantidade de parcelas.
        /// </summary>
        public int Installments { get; set; }
        /// <summary>
        /// Valor de cada parcela, em centavos.
        /// </summary>
        [JsonPropertyName("installment_value")]
        public int InstallmentValue { get; set; }
        /// <summary>
        /// Indica se o parcelamento é isento de juros para o comprador.
        /// </summary>
        [JsonPropertyName("interest_free")]
        public bool InterestFree { get; set; }
        /// <summary>
        /// Valor total do plano de parcelamento e suas taxas.
        /// </summary>
        public Amount? Amount { get; set; }
    }
}
