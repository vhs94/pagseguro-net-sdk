using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// QR Code Pix gerado para o pedido.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pedido-com-qr-code-pix-v2">ler documentação</see>
    /// </summary>
    public class QrCodeResponse : QrCodeBase
    {
        /// <summary>
        /// Identificador do QR Code.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Código copia e cola do Pix.
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// Links para as imagens do QR Code.
        /// </summary>
        public ICollection<Link> Links { get; set; }

        public QrCodeResponse() => Links = [];
    }
}
