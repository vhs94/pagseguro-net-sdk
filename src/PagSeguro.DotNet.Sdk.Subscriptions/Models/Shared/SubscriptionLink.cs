namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Link relacionado ao recurso.
    /// </summary>
    public class SubscriptionLink
    {
        /// <summary>Tipo de relacionamento do link. Por exemplo, SELF.</summary>
        public string? Rel { get; set; }

        /// <summary>Endereço HTTP do recurso.</summary>
        public string? Href { get; set; }

        /// <summary>Tipo de mídia do recurso.</summary>
        public string? Media { get; set; }

        /// <summary>Verbo HTTP aceito pelo link.</summary>
        public string? Type { get; set; }
    }
}
