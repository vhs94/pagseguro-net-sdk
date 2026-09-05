namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Periodicidade de cobrança do plano.
    /// </summary>
    public class PlanInterval
    {
        /// <summary>
        /// Unidade do intervalo. Valores possíveis: DAY, MONTH e YEAR.
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>Duração do intervalo na unidade informada.</summary>
        public int Length { get; set; }
    }
}
