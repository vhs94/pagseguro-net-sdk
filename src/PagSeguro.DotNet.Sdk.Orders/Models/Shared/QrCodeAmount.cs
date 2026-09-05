namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Valor associado ao QR Code Pix.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pedido-com-qr-code-pix-v2">ler documentação</see>
    /// </summary>
    public class QrCodeAmount
    {
        /// <summary>
        /// Valor do QR Code, em centavos.
        /// </summary>
        public int Value { get; set; }
    }
}
