using PagSeguro.DotNet.Sdk.Common.Serialization;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Account.Models.Shared
{
    /// <summary>
    /// Dados pessoais do dono da conta ou do sócio da empresa.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-account">ler documentação</see>
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Data de nascimento do usuário ou do sócio.
        /// </summary>
        [JsonPropertyName("birth_date")]
        [JsonConverter(typeof(ShortDateConverter))]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Nome completo do usuário ou do sócio. Máximo de 50 caracteres.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Nome da mãe do dono da conta ou do sócio. Máximo de 50 caracteres.
        /// </summary>
        [JsonPropertyName("mother_name")]
        public string? MotherName { get; set; }
        /// <summary>
        /// CPF do dono da conta ou do sócio.
        /// </summary>
        [JsonPropertyName("tax_id")]
        public string? TaxId { get; set; }
        /// <summary>
        /// Endereço do dono da conta ou do sócio.
        /// </summary>
        public Address? Address { get; set; }
        /// <summary>
        /// Lista de telefones do dono da conta ou do sócio.
        /// </summary>
        public ICollection<Phone> Phones { get; set; }

        public Person() => Phones = [];
    }
}
