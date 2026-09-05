namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Informações de paginação devolvidas pelas listagens da API de Assinaturas.
    /// </summary>
    public class ResultSet
    {
        /// <summary>Quantidade total de registros encontrados.</summary>
        public int Total { get; set; }

        /// <summary>Deslocamento aplicado na listagem.</summary>
        public int Offset { get; set; }

        /// <summary>Quantidade máxima de registros por página.</summary>
        public int Limit { get; set; }
    }
}
