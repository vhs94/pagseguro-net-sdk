namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Dimensões físicas do item, utilizadas no cálculo do frete.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public class Dimension
    {
        /// <summary>
        /// Comprimento do item, em centímetros.
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Largura do item, em centímetros.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Altura do item, em centímetros.
        /// </summary>
        public int Height { get; set; }
    }
}
