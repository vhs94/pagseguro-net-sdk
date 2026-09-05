using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Planos de parcelamento disponíveis para uma bandeira de cartão.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    public class CreditCardBrand
    {
        /// <summary>
        /// Opções de parcelamento retornadas para a bandeira.
        /// </summary>
        [JsonPropertyName("installment_plans")]
        public ICollection<InstallmentPlan> InstallmentPlans { get; set; }

        public CreditCardBrand() => InstallmentPlans = [];
    }
}
