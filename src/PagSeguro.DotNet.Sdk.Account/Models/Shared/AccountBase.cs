using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Shared
{
    /// <summary>
    /// Dados comuns de uma conta PagBank, compartilhados entre a criação e a consulta.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-account">ler documentação</see>
    /// </summary>
    public abstract class AccountBase
    {
        /// <summary>
        /// Tipo de conta a ser criada. Valores possíveis: BUYER, SELLER e ENTERPRISE.
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// E-mail utilizado no login da conta. Máximo de 60 caracteres.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Classificação do negócio.
        /// Obrigatório para contas do tipo SELLER e ENTERPRISE.
        /// </summary>
        [JsonPropertyName("business_category")]
        public string? BusinessCategory { get; set; }
        /// <summary>
        /// Dados do dono da conta ou do sócio.
        /// </summary>
        public Person? Person { get; set; }
    }
}
