namespace PagSeguro.DotNet.Sdk.Checkout.Models.Shared
{
    /// <summary>
    /// Telefone do cliente informado no checkout.
    /// <see href="https://developer.pagbank.com.br/reference/criar-checkout">ler documentação</see>
    /// </summary>
    public class CheckoutPhone
    {
        /// <summary>
        /// Código de operadora do País (DDI).
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Código de operadora local (DDD).
        /// </summary>
        public string? Area { get; set; }

        /// <summary>
        /// Número do telefone.
        /// </summary>
        public string? Number { get; set; }
    }
}
