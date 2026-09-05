namespace PagSeguro.DotNet.Sdk.Checkout.Models.Shared
{
    /// <summary>
    /// Link relacionado ao checkout. A relação PAY aponta para a página de
    /// pagamento que deve ser aberta pelo comprador.
    /// <see href="https://developer.pagbank.com.br/reference/criar-checkout">ler documentação</see>
    /// </summary>
    public class CheckoutLink
    {
        /// <summary>
        /// Tipo de relacionamento do link. Por exemplo, SELF, PAY, ACTIVATE e INACTIVATE.
        /// </summary>
        public string? Rel { get; set; }

        /// <summary>
        /// Endereço HTTP do recurso.
        /// </summary>
        public string? Href { get; set; }

        /// <summary>
        /// Verbo HTTP aceito pelo link.
        /// </summary>
        public string? Method { get; set; }
    }
}
