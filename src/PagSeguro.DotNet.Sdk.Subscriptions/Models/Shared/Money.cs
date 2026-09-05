namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Valor monetário usado nos recursos de Assinaturas.
    /// </summary>
    public class Money
    {
        /// <summary>Valor em centavos.</summary>
        public int Value { get; set; }

        /// <summary>Código de moeda no padrão ISO. Atualmente apenas BRL.</summary>
        public string? Currency { get; set; }
    }
}
