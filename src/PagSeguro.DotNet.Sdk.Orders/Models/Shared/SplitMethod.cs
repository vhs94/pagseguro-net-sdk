namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Como os valores informados em cada recebedor são interpretados.
    /// <see href="https://developer.pagbank.com.br/docs/config-split">ler documentação</see>
    /// </summary>
    public static class SplitMethod
    {
        /// <summary>Os valores são absolutos, em centavos.</summary>
        public const string Fixed = "FIXED";

        /// <summary>Os valores são percentuais do total da cobrança.</summary>
        public const string Percentage = "PERCENTAGE";
    }
}
