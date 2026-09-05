using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Telefone do assinante.</summary>
    public class CustomerPhone
    {
        /// <summary>Identificador do telefone, atribuído pelo PagBank na resposta.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; set; }

        /// <summary>Código do País (DDI). Atualmente apenas 55.</summary>
        public string? Country { get; set; }

        /// <summary>Código local (DDD). Máximo de 3 caracteres.</summary>
        public string? Area { get; set; }

        /// <summary>Número do telefone. Máximo de 9 caracteres.</summary>
        public string? Number { get; set; }
    }
}
