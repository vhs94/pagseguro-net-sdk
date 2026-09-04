namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Valor da cobrança.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public abstract class ChargeAmountBase
    {
        /// <summary>
        /// Valor a ser cobrado, em centavos. Somente inteiros positivos.
        /// Por exemplo, R$ 1.500,99 corresponde a 150099.
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Código de moeda no padrão ISO. Atualmente apenas BRL é suportado.
        /// </summary>
        public string? Currency { get; set; }
    }
}
