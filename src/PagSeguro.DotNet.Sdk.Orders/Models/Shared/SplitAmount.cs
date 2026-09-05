namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Parcela destinada a um recebedor. É interpretada em centavos quando o
    /// método da divisão é FIXED, e como percentual quando é PERCENTAGE.
    /// <see href="https://developer.pagbank.com.br/docs/config-split">ler documentação</see>
    /// </summary>
    public class SplitAmount
    {
        /// <summary>Valor em centavos, ou percentual, conforme o método da divisão.</summary>
        public int Value { get; set; }
    }
}
