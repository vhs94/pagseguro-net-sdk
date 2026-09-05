namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Contém as informações de links relacionados ao recurso (HATEOAS).
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Tipo de relacionamento do link com o recurso. Por exemplo, SELF.
        /// </summary>
        public string? Rel { get; set; }
        /// <summary>
        /// Endereço HTTP do recurso. De 5 a 2048 caracteres.
        /// </summary>
        public string? Href { get; set; }
        /// <summary>
        /// Tipo de mídia do recurso. De 11 a 64 caracteres.
        /// </summary>
        public string? Media { get; set; }
        /// <summary>
        /// Verbo HTTP aceito pelo link. Valores possíveis: GET, POST, PUT e DELETE.
        /// </summary>
        public string? Type { get; set; }
    }
}
